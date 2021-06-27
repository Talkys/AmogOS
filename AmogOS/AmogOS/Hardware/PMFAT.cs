using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using AmogOS.Core;

namespace AmogOS.Hardware
{
    class PMFAT
    {
        /*Driver*/
        public static CosmosVFS Driver;

        /*Diretórios*/
        public static string CurrentDirectory = @"0:\";
        public static string ConfigFile = @"0:\SYSTEM\USERCFG.PMC";

        /*Inicialização*/
        public static void Initialize()
        {
            try
            {
                // Inicializar Driver
                Driver = new CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(Driver);

                // Checar caminho de diretório
                if (!FolderExists(@"0:\SYSTEM")) { CreateFolder(@"0:\SYSTEM"); }

                // Criar arquivo de configuração se não houver um
                if (!FileExists(ConfigFile)) { WriteAllText(ConfigFile, SystemInfo.DefaultConfig); }
            }
            // Se der merda chama aqui
            catch (Exception ex)
            {
                Console.WriteLine("Error intializing FAT file system!");
                Console.WriteLine("[INTERNAL] " + ex.Message);
            }
        }

        /*Pegar arquivos*/
        public static string[] GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }

        /*Pegar pastas*/
        public static string[] GetFolders(string path)
        {
            string[] folders = Directory.GetDirectories(path);
            return folders;
        }

        /*Pegar volumes*/
        public static List<Sys.FileSystem.Listing.DirectoryEntry> GetVolumes()
        {
            return Driver.GetVolumes();
        }

        /*Verificar existência de arquivo*/
        public static bool FileExists(string file) { return File.Exists(file); }
        public static bool FolderExists(string path) { return Directory.Exists(path); }

        /*Leitura*/
        public static string[] ReadLines(string path)
        {
            string[] data;
            data = File.ReadAllLines(path);
            return data;
        }
        public static string ReadText(string path)
        {
            string data;
            data = File.ReadAllText(path);
            return data;
        }
        public static byte[] ReadBytes(string path)
        {
            byte[] data;
            data = File.ReadAllBytes(path);
            return data;
        }

        /*Escrita*/
        public static void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        public static void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }
        public static void WriteAllBytes(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }
        public static void WriteAllBytes(string path, List<byte> data)
        {
            byte[] input = new byte[data.Count];
            for (int i = 0; i < input.Length; i++) { input[i] = data[i]; }
            WriteAllBytes(path, input);
        }

        /*Criação*/
        public static bool CreateFolder(string name)
        {
            bool value = false;
            if (FolderExists(name)) { value = false; }
            else { Directory.CreateDirectory(name); value = true; }
            return value;
        }

        /*Renomeação de diretório*/
        public static bool RenameFolder(string input, string newName)
        {
            bool value = false;
            if (Directory.Exists(input))
            { Directory.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }

        /*Renomear arquivo*/
        public static bool RenameFile(string input, string newName)
        {
            bool value = false;
            if (FileExists(input))
            { File.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }

        /*Deletar diretório*/
        public static bool DeleteFolder(string path)
        {
            if (FolderExists(path))
            {
                try
                {
                    CLI.WriteLine("Attempting to delete \"" + path + "\"", Graphics.Color.Yellow);
                    Directory.Delete(path, true);
                    if (!FolderExists(path)) { return true; }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    CLI.Write("[INTERNAL] ", Graphics.Color.Red); CLI.WriteLine(ex.Message, Graphics.Color.White);
                    return false;
                }
            }
            else { return false; }
        }

        /*Deletar arquivo*/
        public static bool DeleteFile(string file)
        {
            if (FileExists(file)) { File.Delete(file); return true; }
            else { return false; }
        }

        /*Obter informações de arquivo*/
        public static Cosmos.System.FileSystem.Listing.DirectoryEntry GetFileInfo(string file)
        {
            if (FileExists(file))
            {
                try
                {
                    Cosmos.System.FileSystem.Listing.DirectoryEntry attr = Driver.GetFile(file);
                    return attr;
                }
                catch (Exception ex)
                {
                    CLI.WriteLine("Error occured when trying to retrieve file info", Graphics.Color.Red);
                    CLI.Write("[INTERNAL] ", Graphics.Color.Red); CLI.WriteLine(ex.Message, Graphics.Color.White);
                    return null;
                }
            }
            else
            {
                CLI.WriteLine("Could not locate file \"" + file + "\"", Graphics.Color.Red);
                return null;
            }
        }

        /*Copiar arquivo*/
        public static bool CopyFile(string src, string dest)
        {
            try
            {
                byte[] sourceData = ReadBytes(src);
                WriteAllBytes(dest, sourceData);
                return true;
            }
            catch (Exception ex)
            {
                CLI.WriteLine("Error occured when trying to copy file", Graphics.Color.Red);
                CLI.Write("[INTERNAL] ", Graphics.Color.Red); CLI.WriteLine(ex.Message, Graphics.Color.White);
                return false;
            }
        }

        /*b para Mb*/
        public static double BytesToMB(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        /*b para kb*/
        public static double BytesToKB(long bytes)
        {
            return bytes / 1024;
        }
    }
}
