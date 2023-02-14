namespace RandomImageGenerator
{
    internal class Program
    {
        private static void ScanSavesDirectory(ref string path, ref int id)
        {
            string[] files = Directory.GetFiles(path);
            if (files.Length == 0) return;
            for (int i = 0; i < files.Length; i++)
            {
                string filename = Path.GetFileName(files[i]);
                try
                {
                    if (filename.Split('.')[1] == "txt")
                    {
                        int value = Convert.ToInt32(filename.Split('.')[0]);
                        if (value >= id) id = value + 1;
                    }
                }
                catch {}
            }
        }

        private static void SaveImage(char[] img)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/saved_images/";
            int id = 1;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            else ScanSavesDirectory(ref path, ref id);
            string imageString = "";
            for (int i = 0; i < img.Length;i++)
            {
                if ((i + 1) % 120 == 0 & i + 1 < img.Length)
                {
                    imageString += $"{img[i]}\n";
                }
                else imageString += img[i];
            }
            File.WriteAllText($"{path}/{id}.txt", imageString);
        }

        private static void RotateImage(ref char[] img)
        {
            char[] tmp = new char[img.Length];
            Array.Copy(img, tmp, img.Length);
            for (int i = 0, j = tmp.Length - 1; i < tmp.Length; i++, j--) img[i] = tmp[j];
        }

        private static void InvertImage(ref char[] img)
        {
            for (int i = 0; i < img.Length; i++)
            {
                if (img[i] == ' ')  img[i] = '#';
                else img[i] = ' '; 
            }
        }

        private static char[] NewImage()
        {
            return new RandomImageGenerator().image;
        }

        static void Main(string[] args)
        {
            Console.Title = "Random image generator";
            Console.TreatControlCAsInput = false;
            Console.Write($"* Random (abstract) image generator by HardcoreMagazine\n" +
                          $"* Github: https://github.com/HardcoreMagazine/RandomImageGenerator\n\n" + 
                          $"* Controls:\n" + 
                          $"** RIGHT ARROW - create new image\n" + 
                          $"** S\t       - save current image in file\n" +
                          $"** I\t       - invert image colors\n" +
                          $"** R\t       - rotate image 180 degrees\n" +
                          $"** ESC\t       - exit program\n\n" +
                          $"* Images saved as text files under current program directory in 'saved_images' subfolder\n\n" +
                          $"* Press any key continue...");
            Console.ReadKey(true);
            char[] img = NewImage();
            while (true)
            {
                Console.Clear();
                Console.Write(img);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.RightArrow:
                        img = NewImage();
                        break;
                    case ConsoleKey.S:
                        SaveImage(img);
                        break;
                    case ConsoleKey.I:
                        InvertImage(ref img);
                        break;
                    case ConsoleKey.R:
                        RotateImage(ref img);
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default: 
                        break;
                }
            }
        }
    }
}