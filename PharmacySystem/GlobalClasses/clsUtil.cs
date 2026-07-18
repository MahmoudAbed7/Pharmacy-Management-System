using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.GlobalClasses
{
    public class clsUtil
    {
        public static string GenerateGuid() 
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static bool CreateFolderIfNotExist(string path) 
        {
            try 
            {
                if (!Directory.Exists(path)) 
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error in creating Folder Operation: " + ex.Message);
                return false;
            }return true;
        }

        public static string ReplaceFileNameWithGuid(string SourceName) 
        {
            FileInfo fi = new FileInfo(SourceName);
            return GenerateGuid() + fi.Extension;   
        }

        public static bool CopyImageToProjectFolder(ref string SourceFile) 
        {
            string DestinationFolder = @"C:\PharmacyImages\";
            if (!CreateFolderIfNotExist(DestinationFolder))
            {
                return false;
            }
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourceFile);
            try 
            {
                File.Copy(SourceFile, DestinationFile, true);
                return true;
            } catch (Exception ex) 
            {
                MessageBox.Show("Error: "+ ex.Message);
                return false;
            }
            SourceFile = DestinationFile;
            return true;
        }
    }
}
