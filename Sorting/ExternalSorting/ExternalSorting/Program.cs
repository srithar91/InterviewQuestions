using ExternalSorting.Heaps;
using ExternalSorting.Sorters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.) Generate a Sample Data");
            Console.WriteLine("2.) Sort");
            Console.WriteLine("Enter ur choice:");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the Size of the dataset (in MB):");

                        int sizeMB = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the folder path (D:\\SampleData):");

                        string folderPath = Console.ReadLine();

                        WriteData(sizeMB, folderPath);

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the file path (D:\\SampleData\\Numbers.bin):");

                        string filePath = Console.ReadLine();

                        Console.WriteLine("Enter the Ram Size (MB):");

                        int ramSize = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the output folder path (D:\\Output):");

                        string outputFolderPath = Console.ReadLine();

                        while (true)
                        {
                            if (File.Exists(filePath))
                            {
                                Sort(filePath, ramSize * 1024 * 1024, outputFolderPath);
                                break;
                            }
                            else
                            {
                                Console.WriteLine(string.Format("{0} file does not exist", filePath));

                                Console.WriteLine("Enter the file path (D:\\SampleData\\Numbers.bin):");
                                filePath = Console.ReadLine();
                            }
                        }
                        break;
                    }
                default:
                    break;
            }

            Console.ReadLine();
        }

        private static void Sort(string fileName, int ramCapacityInBytes, string outputFolderPath)
        {
            DateTime start = DateTime.Now;

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);

            long fileSize = fileInfo.Length;

            long nChunks = fileSize / ramCapacityInBytes;

            long lastChunkSize = fileSize - (nChunks * ramCapacityInBytes);

            string folderPath = "C:\\Temp\\SplitFiles";

            Console.WriteLine("Spliting and Sorting files...");

            nChunks = SplitFiles(fileName, (int)nChunks, ramCapacityInBytes, (int)lastChunkSize, folderPath);

            Console.WriteLine(string.Format("Spliting and Sorting files Completed. Time taken {0} seconds", (DateTime.Now - start).TotalSeconds));

            Console.WriteLine("Merging results...");

            FileAccessor accessor = new FileAccessor();

            List<string> fileNames = Directory.GetFiles(folderPath).ToList();

            foreach (string name in fileNames)
            {
                accessor.AddFileChunk(name);
            }

            if (!Directory.Exists(outputFolderPath))
                Directory.CreateDirectory(outputFolderPath);

            using (FileStream stream = new FileStream(string.Format("{0}\\Output.bin", outputFolderPath), FileMode.OpenOrCreate))
            {
                MinHeap heap = new MinHeap();

                for (int chunk = 0; chunk < nChunks; chunk++)
                {
                    int val = 0;

                    bool isAvailable = accessor.TryGetNextIntFromFile(chunk, ref val);

                    if (isAvailable)
                    {
                        heap.Insert(val, chunk);
                    }
                }

                while (accessor.DataAvailable)
                {
                    HeapNode node = heap.ExtractTop();

                    //Debug.WriteLine(node.val);

                    int val = 0;

                    bool isAvailable = accessor.TryGetNextIntFromFile(node.Chunk, ref val);

                    if (isAvailable)
                    {
                        heap.Insert(val, node.Chunk);
                    }

                    byte[] number = new byte[4];

                    number[0] = (byte)(node.Value >> 24);
                    number[1] = (byte)(node.Value >> 16);
                    number[2] = (byte)(node.Value >> 8);
                    number[3] = (byte)node.Value;

                    stream.Write(number, 0, 4);
                }
            }

            Console.WriteLine(string.Format("Completed. Total time taken {0} Seconds", (DateTime.Now - start).TotalSeconds));
        }

        private static int SplitFiles(string fileName, int nChunks, int ramCapacityInBytes, int lastChunkSize, string folderPath)
        {
            byte[] byteArray = new byte[ramCapacityInBytes];

            int offset = 0;

            SortBase sortAlgo = new QuickSort();

            using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                for (int i = 0; i < nChunks; i++)
                {
                    stream.Seek(offset, System.IO.SeekOrigin.Begin);

                    stream.Read(byteArray, 0, ramCapacityInBytes);

                    int[] intArray = ConvertToInt(byteArray);

                    sortAlgo.Sort(intArray);

                    string savedFileName = WriteToFile(intArray, i, folderPath);

                    Console.WriteLine(string.Format("File {0} Size {1} bytes ({2})", i, ramCapacityInBytes, savedFileName));

                    offset += ramCapacityInBytes;
                }

                if (lastChunkSize > 0)
                {
                    byteArray = new byte[lastChunkSize];

                    stream.Seek(offset, System.IO.SeekOrigin.Begin);

                    stream.Read(byteArray, 0, lastChunkSize);

                    int[] intArray = ConvertToInt(byteArray);

                    sortAlgo.Sort(intArray);

                    string savedFileName = WriteToFile(intArray, nChunks, folderPath);

                    Console.WriteLine(string.Format("File {0} Size {1} bytes ({2})", nChunks, lastChunkSize, savedFileName));

                    nChunks += 1;
                }
            }

            return nChunks;
        }

        private static int[] ConvertToInt(byte[] byteArray)
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < byteArray.Length; i += 4)
            {
                int nums = (byteArray[i] << 24) | (byteArray[i + 1] << 16) | (byteArray[i + 2] << 8) | (byteArray[i + 3]);
                numbers.Add(nums);
            }

            return numbers.ToArray();
        }

        private static string WriteToFile(int[] arr, int seqNumber, string folderPath)
        {
            byte[] number = new byte[4];

            string fileName = string.Format("{0}\\Number_{1}.bin", folderPath, seqNumber);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate))
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    number[0] = (byte)(arr[i] >> 24);
                    number[1] = (byte)(arr[i] >> 16);
                    number[2] = (byte)(arr[i] >> 8);
                    number[3] = (byte)arr[i];

                    stream.Write(number, 0, 4);
                }
            }

            return fileName;
        }

        private static void WriteData(int sizeMB, string outputFolderPath)
        {
            Random rand = new Random();

            byte[] number = new byte[4];

            if (!Directory.Exists(outputFolderPath))
                Directory.CreateDirectory(outputFolderPath);

            using (System.IO.FileStream stream = new System.IO.FileStream(string.Format("{0}\\Numbers.bin", outputFolderPath), System.IO.FileMode.OpenOrCreate))
            {
                long n = (sizeMB * 1024 * 1024) / 4;

                for (long i = 0; i < n; i++)
                {
                    int num = rand.Next(1, (int)Math.Pow(2, 24));

                    number[0] = (byte)(num >> 24);
                    number[1] = (byte)(num >> 16);
                    number[2] = (byte)(num >> 8);
                    number[3] = (byte)num;

                    stream.Write(number, 0, 4);
                }
            }
        }
    }

    public class FileAccessor
    {
        private List<FilePointer> filePointers;

        public FileAccessor()
        {
            this.filePointers = new List<FilePointer>();
        }

        public bool DataAvailable
        {
            get
            {
                return filePointers.Any(pointer => !pointer.IsEnd());
            }
        }

        public void AddFileChunk(string fileName)
        {
            FilePointer pointer = new FilePointer(fileName);
            filePointers.Add(pointer);
        }

        public bool TryGetNextIntFromFile(int chunk, ref int value)
        {
            if (chunk > filePointers.Count - 1)
                return false;

            if (filePointers[chunk].IsEnd())
                return false;

            value = filePointers[chunk].GetNextInt();
            return true;
        }
    }

    public class FilePointer
    {
        public string FileName;
        public long Offset;
        public long Length;

        private FileStream Stream;

        public FilePointer(string fileName)
        {
            this.FileName = fileName;
            this.Stream = new FileStream(fileName, FileMode.Open);
            System.IO.FileInfo fileInfo = new FileInfo(FileName);
            this.Offset = 0;
            this.Length = fileInfo.Length;
        }

        public bool IsEnd()
        {
            if ((this.Offset + 4) > this.Length)
                return true;
            else
                return false;
        }

        public int GetNextInt()
        {
            byte[] byteArr = new byte[4];
            this.Stream.Seek(Offset, SeekOrigin.Begin);
            this.Stream.Read(byteArr, 0, 4);
            this.Offset += 4;

            if ((this.Offset + 4) > this.Length)
                Stream.Dispose();

            return (byteArr[0] << 24) | (byteArr[1] << 16) | (byteArr[2] << 8) | (byteArr[3]);
        }
    }
}
