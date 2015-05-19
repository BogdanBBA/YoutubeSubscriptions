using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace YoutubeSubscriptions.Classes
{
    public static class Utils
    {
        private static string ColonReplacement = "XcolonX";
        public static string DateTimeFormatA = "d MMMM yyyy 'at' H'H'";
        public static string DateTimeFormatA2 = "dddd, d MMMM yyyy 'at' H'H'";
        public static string DateTimeFormatB = "d MMMM yyyy 'at' HH:mm";
        public static string DateTimeFormatD = "d MMMM yyyy";
        public static string DateTimeFormatT = "HH:mm:ss / dddd, d MMMM yyyy";

        public const string EarningsCurrency = "$";
        public const double MinimumCPM = 0.3;
        public const double MaximumCPM = 2.5;

        public static TDatabase CreateInitializationDatabase()
        {
            TVideo vid = new TVideo();
            vid.ID = "id";
            vid.Uploaded = DateTime.Now;
            vid.Title = "title";
            vid.Duration = 120;
            vid.Views = 10000;
            vid.Likes = 100;
            vid.Dislikes = 20;
            vid.AvgRating = 4.3;
            vid.Comments = 17;
            TYoutuber yt = new TYoutuber();
            yt.ID = "id";
            yt.Name = "name";
            yt.InfoLastUpdated = DateTime.Now;
            yt.Joined = DateTime.Now.AddYears(-1);
            yt.Subscribers = 42000;
            yt.TotalVideos = 231;
            yt.TotalViews = 175000;
            yt.Videos = new List<TVideo>(new TVideo[1] { vid });
            TDatabase db = new TDatabase();
            db.Youtubers = new List<TYoutuber>(new TYoutuber[1] { yt });
            return db;
        }

        public static DateTime DecodeYoutubeApiDate(string encodedDate)
        {
            int y = 2000, M = 1, d = 1, h = 0, m = 0, s = 0;
            try
            {
                y = Int32.Parse(encodedDate.Substring(0, 4));
                M = Int32.Parse(encodedDate.Substring(5, 2));
                d = Int32.Parse(encodedDate.Substring(8, 2));
                h = Int32.Parse(encodedDate.Substring(11, 2));
                m = Int32.Parse(encodedDate.Substring(14, 2));
                s = Int32.Parse(encodedDate.Substring(17, 2));
            }
            catch (Exception E)
            { }
            return new DateTime(y, M, d, h, m, s);
        }

        public static void EncodeColonsInFile(string filePath)
        {
            File.WriteAllText(filePath, EncodeColons(File.ReadAllText(filePath)));
        }

        public static string EncodeColons(string text)
        {
            return text;//text.Replace(":", ColonReplacement);
        }

        public static string DecodeColons(string text)
        {
            return text.Replace(ColonReplacement, ":");
        }

        public static List<string> ParseDelimitedString(string arguments, char delim = ',')
        {
            bool inQuotes = false;
            bool inNonQuotes = false;
            int whiteSpaceCount = 0;

            List<string> strings = new List<string>();

            StringBuilder sb = new StringBuilder();
            foreach (char c in arguments)
            {
                if (c == '\'' || c == '"')
                {
                    if (!inQuotes)
                        inQuotes = true;
                    else
                        inQuotes = false;

                    whiteSpaceCount = 0;
                }
                else if (c == delim)
                {
                    if (!inQuotes)
                    {
                        if (whiteSpaceCount > 0 && inQuotes)
                        {
                            sb.Remove(sb.Length - whiteSpaceCount, whiteSpaceCount);
                            inNonQuotes = false;
                        }
                        strings.Add(sb.Replace("'", string.Empty).Replace("\"", string.Empty).ToString());
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    whiteSpaceCount = 0;
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (inNonQuotes || inQuotes)
                    {
                        sb.Append(c);
                        whiteSpaceCount++;
                    }
                }
                else
                {
                    if (!inQuotes)
                        inNonQuotes = true;
                    sb.Append(c);
                    whiteSpaceCount = 0;
                }
            }
            strings.Add(sb.Replace("'", string.Empty).Replace("\"", string.Empty).ToString());
            return strings;
        }

        public static XmlAttribute GetNewAttribute(XmlDocument doc, string name, string value)
        {
            XmlAttribute attr = doc.CreateAttribute(name);
            attr.Value = value;
            return attr;
        }

        public static XmlAttribute GetNewAttribute(XmlDocument doc, string name, int value)
        {
            return GetNewAttribute(doc, name, value.ToString());
        }

        public static Size ScaleRectangle(int width, int height, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth / width;
            var ratioY = (double) maxHeight / height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (width * ratio);
            var newHeight = (int) (height * ratio);

            return new Size(newWidth, newHeight);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, bool disposeOldImage)
        {
            Size newSize = ScaleRectangle(image.Width, image.Height, maxWidth, maxHeight);
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            if (disposeOldImage)
                image.Dispose();
            return newImage;
        }

        public static string RegularPlural(string singularForm, long quantity, bool includeQuantity)
        {
            string result = includeQuantity ? FormatNumber(quantity) + " " + singularForm : singularForm;
            return quantity == 1 ? result : result + "s";
        }

        public static string FormatNumberOrder(long number)
        {
            if (number == 0)
                return "0";
            number = Math.Abs(number);
            string[] ordChr = { "", "K", "M", "B" };
            int powOrd = 3;
            while (powOrd >= 0)
            {
                long x = (long) Math.Pow(1000, powOrd);
                if (number >= x)
                    return ((float) number / x).ToString("n" + powOrd) + ordChr[powOrd];
                powOrd--;
            }
            return "???";
        }

        public static string FormatNumber(long number)
        {
            return number.ToString("#,##0");
        }

        public static string FormatDateTimeByFormatIndex(DateTime targetDateTime, int settingsFormatIndex)
        {
            // check with TSettings.UploadedFormats
            switch (settingsFormatIndex)
            {
                case 0:
                case 1:
                case 2:
                    string temp = TSettings.UploadedFormats[settingsFormatIndex].Replace(TSettings.TimePassedFormatString, "\"" + FormatElapsedTime(DateTime.Now.Subtract(targetDateTime)) + "\"");
                    if (settingsFormatIndex == 0)
                        return temp.Replace("\"", "");
                    temp = FormatDateTime(targetDateTime, temp);
                    return temp.Replace("\"", "");
                default:
                    return FormatDateTime(targetDateTime, TSettings.UploadedFormats[settingsFormatIndex]);
            }
        }

        public static string FormatDateTime(DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string FormatDuration(long seconds)
        {
            return string.Format("{0}:{1}", seconds / 60, (seconds % 60).ToString("00"));
        }

        public static string FormatTimeSpan(TimeSpan timeSpan)
        {
            string result = timeSpan.Hours == 0 ? "" : timeSpan.Hours.ToString() + " hrs ";
            result = timeSpan.Minutes == 0 ? result : result + timeSpan.Minutes.ToString() + " min ";
            result = timeSpan.Seconds == 0 ? result : result + timeSpan.Seconds.ToString() + " sec ";
            return result.Equals("") ? "just now" : result.Substring(0, result.Length - 1);
        }

        public static string FormatElapsedTime(TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0)
                return timeSpan.ToString();
            if (timeSpan.Days != 0)
                return timeSpan.Days == 1 ? "one day ago" : timeSpan.Days + " days ago";
            if (timeSpan.Hours != 0)
                return timeSpan.Hours == 1 ? "one hour ago" : timeSpan.Hours + " hours ago";
            if (timeSpan.Minutes != 0)
                return timeSpan.Minutes == 1 ? "one minute ago" : timeSpan.Minutes + " minutes ago";
            if (timeSpan.Seconds != 0)
                return timeSpan.Seconds == 1 ? "one second ago" : timeSpan.Seconds + " seconds ago";
            return "just now";
        }

        public static string FormatEarnings(double amount)
        {
            return amount.ToString("n" + (amount >= 1000 ? 0 : 2)) + " " + Utils.EarningsCurrency;
        }

        public static string FormatMinMaxEarnings(long views)
        {
            double min = Utils.CalculateMinimumEarnings(views), max = Utils.CalculateMaximumEarnings(views);
            // kept separate from FormatEarnings(amount) because this should rank both values on the same order of magnitude
            string nDecimals = "n" + (max >= 1000 ? 0 : 2);
            return string.Format("{0} {2} - {1} {2}", min.ToString(nDecimals), max.ToString(nDecimals), Utils.EarningsCurrency);
        }

        public static double CalculateMinimumEarnings(long views)
        {
            return (double) views / 1000 * MinimumCPM;
        }

        public static double CalculateMaximumEarnings(long views)
        {
            return (double) views / 1000 * MaximumCPM;
        }

        public static int GetTitleHeight(int videoColumns, int titleRowHeight)
        {
            switch (videoColumns)
            {
                case 1:
                case 2:
                    return titleRowHeight;
                case 3:
                case 4:
                    return 2 * titleRowHeight;
                case 5:
                case 6:
                case 7:
                    return 3 * titleRowHeight;
                default:
                    return videoColumns < 1 ? titleRowHeight : 4 * titleRowHeight;
            }
        }        
    }

    public static class Paths
    {
        public const string ProgramFilesFolder = @"..\..\..\program-files\";
        public const string GDataXmlFolder = ProgramFilesFolder + @"gdata-xml\";
        public const string ThumbnailFolder = ProgramFilesFolder + @"thumbnails\";
        public const string AppAssetsFolder = ProgramFilesFolder + @"app-assets\";
        public const string DatabaseFile = AppAssetsFolder + @"database.xml";
        public const string SettingsFile = AppAssetsFolder + @"settings.xml";
        public const string StarImage = AppAssetsFolder + @"star.png";
        
        private static readonly string[] FoldersToCheck = new string[4] { ProgramFilesFolder, GDataXmlFolder, ThumbnailFolder, AppAssetsFolder };
        private static readonly string[] FilesToCheck = new string[3] { DatabaseFile, SettingsFile, StarImage };

        public static string CheckFolders(bool tryToCreateMissingFoldersOnce)
        {
            foreach (string folder in FoldersToCheck)
            {
                string checkResult = CheckFolder(folder, tryToCreateMissingFoldersOnce);
                if (!checkResult.Equals(""))
                    return checkResult;
            }
            foreach (string file in FilesToCheck)
            {
                string checkResult = CheckFile(file);
                if (!checkResult.Equals(""))
                    return checkResult;
            }
            return "";
        }

        private static string CheckFolder(string folder, bool tryToCreateMissingFolderOnce)
        {
            try
            {
                if (Directory.Exists(folder))
                    return "";
                if (tryToCreateMissingFolderOnce)
                {
                    Directory.CreateDirectory(folder);
                    return Directory.Exists(folder) ? "" : "The folder \"" + folder + "\" does not exist and could not be created!";
                }
                else
                    return "The folder \"" + folder + "\" does not exist!";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        private static string CheckFile(string file)
        {
            try
            {
                if (File.Exists(file))
                    return "";
                return "The file \"" + file + "\" does not exist!";
            }
            catch (Exception E)
            { return E.ToString(); }
        }
    }
}
