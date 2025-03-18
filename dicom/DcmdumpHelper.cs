/*
 * Created by SharpDevelop.
 * User: Tom Esparon
 * Date: 18/03/2025
 * Time: 14:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Reflection;

namespace dicom  // Make sure this matches your project's namespace
{
    public class DcmdumpHelper
    {
        public static string ExtractDcmdump()
        {
            // Extract to the application's directory
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string dcmdumpPath = Path.Combine(appDir, "dcmdump.exe");

            // Only extract if the file doesn't exist
            if (!File.Exists(dcmdumpPath))
            {
                using (Stream resourceStream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("dicom.dcmdump.exe")) // Ensure resource name is correct
                using (FileStream fileStream = new FileStream(dcmdumpPath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }

            return dcmdumpPath;
        }
    }
}