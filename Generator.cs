using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;
using BCnEncoder.Encoder;
using BCnEncoder.Shared;
using BCnEncoder.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Fallout4;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Records;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PSTK;

public static class Generator
{
    private static Label _progressLabel;
    private static List<GeneratorItem> _items = new();
    private static List<GeneratorItem> _badItems = new();
    private static string _outDir = string.Empty;
    private static BcEncoder _ddsEncoder = new BcEncoder();
    private static BGSM BGSM = new BGSM();
    public static string OutputDirectory => _outDir;
    public static string TextureDirectory(string packname) => Path.Combine(Generator.OutputDirectory, "Tx", "Textures", "PipSaver", packname);
    public static string MaterialDirectory(string packname) => Path.Combine(Generator.OutputDirectory, "Mn", "Materials", "PipSaver", packname);

    public static void Generate(string author, string packname, string desc, Label progressLabel, Button reEnable, List<string> filePaths)
    {
        Form f = progressLabel.FindForm();
        _ddsEncoder.OutputOptions.GenerateMipMaps = true;
        _ddsEncoder.OutputOptions.Quality = CompressionQuality.Balanced;
        _ddsEncoder.OutputOptions.Format = CompressionFormat.Bc1;
        _ddsEncoder.OutputOptions.FileFormat = OutputFileFormat.Dds;
        _ddsEncoder.OutputOptions.MaxMipMapLevel = 4;

        string authorPlug = author.Substring(0, Math.Min(author.Length, 10)).Replace(" ", "_");
        string modPlug = packname.Substring(0, Math.Min(packname.Length, 10)).Replace(" ", "_");
        string edIdPlug = "ps_" + authorPlug + "_" + modPlug;

        _outDir = Path.Combine(Path.Combine(AppContext.BaseDirectory, "output"), packname);
        string _bsArchPath = Path.Combine(AppContext.BaseDirectory, "BSArch.exe");
        if (!File.Exists(_bsArchPath))
        {
            MessageBox.Show("Error: BSArch.exe not found.");
            return;
        }
        try
        {
            Directory.CreateDirectory(_outDir);
            Directory.CreateDirectory(TextureDirectory(packname));
            Directory.CreateDirectory(MaterialDirectory(packname));
        } catch (Exception e)
        {
            MessageBox.Show("Could not create output directory:\n" + e.Message);
            throw new Exception("Could not create output directory\n" + e.Message);
        }
        _items.Clear();
        _badItems.Clear();
        _progressLabel = progressLabel;

        int index = 0;
        List<GeneratorItem> items = new();
        int total = filePaths.Count;
        foreach(var fp in filePaths)
        {
            SetProgress("Processing image " + (index + 1) + " of " + total + "...");
            f.Refresh();
            items.Add(new GeneratorItem(packname, index, fp));
            index++;
        }

        //create the plugin
        SetProgress("Creating plugin data...");
        f.Refresh();
        var newMod = new Fallout4Mod(ModKey.FromFileName(packname + ".esp"), Fallout4Release.Fallout4);
        newMod.ModHeader.Flags |= Fallout4ModHeader.HeaderFlag.Light;
        newMod.ModHeader.MasterReferences.Add(new MasterReference() { Master = new ModKey("Fallout4", ModType.Master) });
        newMod.ModHeader.MasterReferences.Add(new MasterReference() { Master = new ModKey("Pipsaver", ModType.Master) });
        newMod.ModHeader.Author = author;
        newMod.ModHeader.Description = desc;

        var colorList = newMod.FormLists.AddNew(edIdPlug + "_Color_InjectionList");
        var grayList = newMod.FormLists.AddNew(edIdPlug + "_Grayscale_InjectionList");

        foreach (var i in items)
        {
            var colorswap = newMod.MaterialSwaps.AddNew(edIdPlug + "_MaterialSwap_Color_PipboyScreenTo" + i.Index);
            colorswap.Substitutions.Add(new MaterialSubstitution() { OriginalMaterial = "AnimObjects\\PipBoy\\PipBoyScreen.BGSM", ReplacementMaterial = i.ColorMaterialPath });
            colorswap.Substitutions.Add(new MaterialSubstitution() { OriginalMaterial = "ScorpTech\\Pipboy 3500\\PipBoyScreen.BGSM", ReplacementMaterial = i.ColorMaterialPath });
            var colormod = new ArmorModification(newMod, edIdPlug + "_Color_ObjectMod_" + i.Index)
            {
                Properties = new Noggog.ExtendedList<AObjectModProperty<Mutagen.Bethesda.Fallout4.Armor.Property>>
                {
                    new ObjectModFormLinkIntProperty<Mutagen.Bethesda.Fallout4.Armor.Property>()
                    {
                        FunctionType = ObjectModProperty.FormLinkFunctionType.Add,
                        Property = Mutagen.Bethesda.Fallout4.Armor.Property.MaterialSwaps,
                        Record = colorswap.FormKey.ToLink<IFallout4MajorRecordGetter>(),
                        Step = 0.0f,
                        Value = 484
                    }
                }
            };
            newMod.ObjectModifications.Add(colormod);
            colorList.Items.Add(colormod);

            var grayswap = newMod.MaterialSwaps.AddNew(edIdPlug + "_MaterialSwap_Grayscale_PipboyScreenTo" + i.Index);
            grayswap.Substitutions.Add(new MaterialSubstitution() { OriginalMaterial = "AnimObjects\\PipBoy\\PipBoyScreen.BGSM", ReplacementMaterial = i.GrayMaterialPath });
            grayswap.Substitutions.Add(new MaterialSubstitution() { OriginalMaterial = "ScorpTech\\Pipboy 3500\\PipBoyScreen.BGSM", ReplacementMaterial = i.GrayMaterialPath });
            var graymod = new ArmorModification(newMod, edIdPlug + "_Grayscale_ObjectMod_" + i.Index)
            {
                Properties = new Noggog.ExtendedList<AObjectModProperty<Mutagen.Bethesda.Fallout4.Armor.Property>>
                {
                    new ObjectModFormLinkIntProperty<Mutagen.Bethesda.Fallout4.Armor.Property>()
                    {
                        FunctionType = ObjectModProperty.FormLinkFunctionType.Add,
                        Property = Mutagen.Bethesda.Fallout4.Armor.Property.MaterialSwaps,
                        Record = grayswap.FormKey.ToLink<IFallout4MajorRecordGetter>(),
                        Step = 0.0f,
                        Value = 484
                    }
                }
            };
            newMod.ObjectModifications.Add(graymod);
            grayList.Items.Add(graymod);
        }


        var quest = new Mutagen.Bethesda.Fallout4.Quest(newMod, edIdPlug + "_Injector")
        {
            VirtualMachineAdapter = new Mutagen.Bethesda.Fallout4.QuestAdapter()
            {
                ObjectFormat = 2,
                Version = 6,
                Scripts = new()
                {
                    new Mutagen.Bethesda.Fallout4.ScriptEntry()
                    {
                        Flags = Mutagen.Bethesda.Fallout4.ScriptEntry.Flag.Local,
                        Name = "Pipsaver:PipInjector",
                        Properties = new()
                        {
                            new Mutagen.Bethesda.Fallout4.ScriptObjectProperty()
                            {
                                Name = "ArtListToAdd",
                                Flags = Mutagen.Bethesda.Fallout4.ScriptProperty.Flag.Edited,
                                Object = colorList.FormKey.ToLink<IFallout4MajorRecordGetter>()
                            },
                            new Mutagen.Bethesda.Fallout4.ScriptObjectProperty()
                            {
                                Name = "GSArtListToAdd",
                                Flags = Mutagen.Bethesda.Fallout4.ScriptProperty.Flag.Edited,
                                Object = grayList.FormKey.ToLink<IFallout4MajorRecordGetter>()
                            },
                            new Mutagen.Bethesda.Fallout4.ScriptObjectProperty()
                            {
                                Name = "PipsaverManager",
                                Flags = Mutagen.Bethesda.Fallout4.ScriptProperty.Flag.Edited,
                                Object = new FormLink<IFallout4MajorRecordGetter>(new FormKey(new ModKey("Pipsaver",ModType.Master),2049))
                            }
                        }
                    }
                }
            },
            Data = new()
            {
                Flags = (Mutagen.Bethesda.Fallout4.Quest.Flag)(0x1 | 0x10 | 0x100),
                Priority = 0,
                DelayTime = 0.0f,
                Type = Mutagen.Bethesda.Fallout4.Quest.TypeEnum.None
            }
        };
        newMod.Quests.Add(quest);

        newMod.WriteToBinary(Path.Combine(_outDir, packname + ".esp"));

        SetProgress("Packing BA2 archives...");
        f.Refresh();

        var startInfo = new ProcessStartInfo();
        startInfo.FileName = _bsArchPath;
        startInfo.Arguments = "pack \"" + Path.Combine(_outDir, "Mn") + "\" \"" + Path.Combine(_outDir, packname + " - Main.ba2") + "\" -fo4 -z";
        var p = Process.Start(startInfo);
        p?.WaitForExit();
        int exitCode = -1;
        if (p != null) exitCode = p.ExitCode;
        if(p == null || p.ExitCode < 0)
        {
            MessageBox.Show("Error packing main archive.\nProgram exited with code: " + exitCode);
            return;
        }

        startInfo = new ProcessStartInfo();
        startInfo.FileName = _bsArchPath;
        startInfo.Arguments = "pack \"" + Path.Combine(_outDir, "Tx") + "\" \"" + Path.Combine(_outDir, packname + " - Textures.ba2") + "\" -fo4dds -z";
        p = Process.Start(startInfo);
        p?.WaitForExit();
        exitCode = -1;
        if (p != null) exitCode = p.ExitCode;
        if (p == null || p.ExitCode < 0)
        {
            MessageBox.Show("Error packing texture archive.\nProgram exited with code: " + exitCode);
            return;
        }

        Directory.Delete(Path.Combine(Generator.OutputDirectory, "Mn"), true);
        Directory.Delete(Path.Combine(Generator.OutputDirectory, "Tx"), true);

        SetProgress("Done! Asset pack created.");
        f.Refresh();

        OpenFolderInExplorer(OutputDirectory);
        reEnable.Enabled = true;
    }

    private static void SetProgress(string message)
    {
        if(_progressLabel != null)
        {
            _progressLabel.Text = message;
        }
    }

    public static void WriteDDS(Image image, string filepath)
    {
        _ddsEncoder.OutputOptions.MaxMipMapLevel = _ddsEncoder.CalculateNumberOfMipLevels(image.Width, image.Height);
        Image<Rgba32> image2 = image.CloneAs<Rgba32>();
        using (FileStream fs = File.OpenWrite(filepath))
        {
            _ddsEncoder.EncodeToStream(image2, fs);
        }
        image2.Dispose();
    }

    public static void WriteMaterial(string imagePath, string filepath)
    {
        using (FileStream fs1 = new FileStream("template.BGSM", FileMode.Open))
        {
            BGSM.Open(fs1);
        }
        imagePath = imagePath.Replace('\\', '/');
        BGSM.DiffuseTexture = imagePath;
        using (FileStream fs2 = new FileStream(filepath, FileMode.Create))
        {
            BGSM.Save(fs2);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public uint fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;
        public int nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon;
        public IntPtr hProcess;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

    private const int SW_SHOW = 5;

    public static bool OpenFolderInExplorer(string folder)
    {
        var info = new SHELLEXECUTEINFO();
        info.cbSize = Marshal.SizeOf<SHELLEXECUTEINFO>();
        info.lpVerb = "explore";
        info.nShow = SW_SHOW;
        info.lpFile = folder;
        return ShellExecuteEx(ref info);
    }
}

public class GeneratorItem
{
    public string ColorMaterialPath { get; set; }
    public string GrayMaterialPath { get; set; }
    public int Index { get; set; }

    public GeneratorItem(string packname, int index, string filepath)
    {
        Index = index;
        var ColorImage = Image.Load(filepath);
        ColorImage.Mutate(x => x.Contrast(0.7f));
        ColorImage.Mutate(x => x.Brightness(0.78f));
        ColorImage.Mutate(x => x.Lightness(0.78f));
        ColorImage.Mutate(x => x.Saturate(0.85f));

        //rescale
        int big = ColorImage.Width;
        int target = 2;
        if (ColorImage.Height > big) big = ColorImage.Height;
        if (big >= 16384)
        {
            target = 16384;
        }
        else if (big >= 8192)
        {
            target = 8192;
        }
        else if (big >= 4096)
        {
            target = 4096;
        }
        else if (big >= 2048)
        {
            target = 2048;
        }
        else if (big >= 1024)
        {
            target = 1024;
        }
        else if (big >= 512)
        {
            target = 512;
        }
        else if (big >= 256)
        {
            target = 256;
        }
        else if (big >= 128)
        {
            target = 128;
        }
        else if (big >= 64)
        {
            target = 64;
        }
        ColorImage.Mutate(x => x.Resize(target, target));
        Generator.WriteDDS(ColorImage, Path.Combine(Generator.TextureDirectory(packname), "ps" + index.ToString() + "_c.dds"));
        Generator.WriteMaterial(Path.Combine("PipSaver", packname, "ps" + index.ToString() + "_c.dds"), Path.Combine(Generator.MaterialDirectory(packname), "ps" + index.ToString() + "_c.BGSM"));
        ColorMaterialPath = Path.Combine("PipSaver", packname, "ps" + index.ToString() + "_c.BGSM").Replace("/", "\\");
        var GrayImage = ColorImage.Clone(x => x.Grayscale());
        var cm = new ColorMatrix
        {
            M11 = 0.8125f,
            M22 = 0.8125f,
            M33 = 0.8125f,
            M51 = 0.09375f,
            M52 = 0.09375f,
            M53 = 0.0625f
        };
        GrayImage.Mutate(x => x.Filter(cm));
        Generator.WriteDDS(GrayImage, Path.Combine(Generator.TextureDirectory(packname), "ps" + index.ToString() + "_g.dds"));
        Generator.WriteMaterial(Path.Combine("PipSaver", packname, "ps" + index.ToString() + "_g.dds"), Path.Combine(Generator.MaterialDirectory(packname), "ps" + index.ToString() + "_g.BGSM"));
        GrayMaterialPath = Path.Combine("PipSaver", packname, "ps" + index.ToString() + "_g.BGSM").Replace("/", "\\");
        ColorImage.Dispose();
        GrayImage.Dispose();
    }
}
