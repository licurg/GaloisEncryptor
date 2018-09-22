using System;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Numerics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace Galois
{
    class GaloisAlgoritm : ObservableObject, IDisposable
    {
        #region Static
        public static int AlgorithmType = 0;
        #endregion
        #region Private
        private Int32 _progress = 0;
        private Int32 _maxProgress = 1;
        private BackgroundWorker worker;

        // Розрядність полінома
        private Int16 length = 64;

        // Режим роботи програми
        private Boolean _encryptMode = true;
        private Boolean _decryptMode = false;
        private Int16 _algorithmIndex = 0;

        // Таймер
        private System.Diagnostics.Stopwatch timer;
        private String _time = "00.00.0000";

        // Disable для елементів
        private Boolean _buttons = true;

        // Шляхи до файлів
        private String _inputFile = "";
        private String _outputFile = "";

        // Обраний поліном
        private Int16 _currentPolynom = 0;
        // OE
        private String _oe = "";
        // VI
        private String _vi = "";
        #endregion
        #region Public
        public Int16 AlgorithmIndex
        {
            get { return _algorithmIndex; }
            set { Set(ref _algorithmIndex, value); }
        }
        // Поліноми
        public String[] Polynoms { get; set; }
        // Обраний поліном
        public Int16 CurrentPolynom
        {
            get { return _currentPolynom; }
            set { Set(ref _currentPolynom, value); }
        }
        // Прогрес 
        public Int32 Progress
        {
            get { return _progress; }
            set { Set(ref _progress, value); }
        }
        public Int32 MaxProgress
        {
            get { return _maxProgress; }
            set { Set(ref _maxProgress, value); }
        }
        // Таймер
        public String Time
        {
            get { return _time; }
            set { Set(ref _time, value); }
        }
        // Disable для елементів
        public Boolean Buttons
        {
            get { return _buttons; }
            set { Set(ref _buttons, value); }
        }
        public Boolean EncryptMode
        {
            get { return _encryptMode; }
            set { Set(ref _encryptMode, value); }
        }
        public Boolean DecryptMode
        {
            get { return _decryptMode; }
            set { Set(ref _decryptMode, value); }
        }
        // Шляхи до файлів
        public String InputFile
        {
            get { return _inputFile; }
            set { Set(ref _inputFile, value); }
        }
        public String OutputFile
        {
            get { return _outputFile; }
            set { Set(ref _outputFile, value); }
        }
        public String OEString
        {
            get { return _oe; }
            set { Set(ref _oe, value); }
        }
        public String VIString
        {
            get { return _vi; }
            set { Set(ref _vi, value); }
        }
        public ICommand OpenInputFile { get; private set; }
        public ICommand OpenOutputFile { get; private set; }
        public ICommand StartGenerateOE { get; private set; }
        public ICommand StartGenerateVI { get; private set; }
        public ICommand StartUploadOE { get; private set; }
        public ICommand StartUploadVI { get; private set; }
        public ICommand StartGaloisAlgorithm { get; private set; }
        public ICommand StartRefresh{ get; private set; }
        #endregion
        #region Constructor
        public GaloisAlgoritm()
        {
            GetPolynoms();
            OpenInputFile = new RelayCommand(OpenInput);
            OpenOutputFile = new RelayCommand(OpenOutput);
            StartGenerateOE = new RelayCommand(GenerateOE);
            StartGenerateVI = new RelayCommand(GenerateVI);
            StartUploadOE = new RelayCommand(UploadOE);
            StartUploadVI = new RelayCommand(UploadVI);
            StartGaloisAlgorithm = new RelayCommand(GaloisAlgorithm);
            StartRefresh = new RelayCommand(Refresh);
        }
        #endregion
        private void OpenInput()
        {
            string path = GetPath();
            if (path == "")
                return;
            if (path == OutputFile)
            {
                OutputFile = "";
            }
            InputFile = path;
        }
        private void OpenOutput()
        {
            string path = SavePath();
            if (path == "")
                return;
            if (path == InputFile)
            {
                InputFile = "";
            }
            OutputFile = path;
        }
        private string GetPath()
        {
            string path = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Всі файли (*.*)|*.*";
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == true)
            {
                path = openFile.FileName;
            }
            return path;
        }
        private string SavePath()
        {
            string path = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Всі файли (*.*)|*.*";
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == true)
            {
                path = saveFile.FileName;
            }
            return path;
        }
        private void GetPolynoms()
        {
            Polynoms = Polynomial.POLYNOMS_64.Select(x => x.ToBinaryString()).ToArray();
        }
        private void UploadOE()
        {
            string fileInfo = GetPath();

            if (fileInfo == "")
                return;
            OEString = File.ReadAllText(fileInfo);
        }
        private void UploadVI()
        {
            string fileInfo = GetPath();
            if (fileInfo == "")
                return;
            VIString = File.ReadAllText(fileInfo);
        }
        private void GenerateOE()
        {
            Buttons = false;
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerDone);
            worker.DoWork += new DoWorkEventHandler(GenerateOE);
            timer = System.Diagnostics.Stopwatch.StartNew();
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        private void GenerateVI()
        {
            Buttons = false;
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerDone);
            worker.DoWork += new DoWorkEventHandler(GenerateVI);
            timer = System.Diagnostics.Stopwatch.StartNew();
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        private void GenerateVI(object sender, DoWorkEventArgs e)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] numBytes = new byte[length / 8];
                rng.GetBytes(numBytes);
                VIString = UInt64String(BitConverter.ToUInt64(numBytes, 0));
            }
            Progress = 1;
        }
        private void GenerateOE(object sender, DoWorkEventArgs e)
        {
            BigInteger f = Polynomial.GetPolynom(CurrentPolynom);
            UInt64 w = 0;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] numBytes = new byte[Polynomial.length / 8];
                rng.GetBytes(numBytes);
                w = BitConverter.ToUInt64(numBytes, 0);
            }
            int lastItem = Polynomial.D.ToList().IndexOf((
                Polynomial.length == 16 ? 21845 : Polynomial.length == 32 ? 1431655765 : Polynomial.length == 64 ? 6148914691236517205 : 85
            ));
            var d = Polynomial.D.Take(lastItem + 1).ToArray();
            MaxProgress = d.Length;
            bool primitive = false;
            while (!primitive)
            {
                bool _continue = false;
                for (int i = 0; i < d.Length; i++)
                {
                    if (Polynomial.ModPow(w, d[i], f) == 1)
                    {
                        _continue = true;
                        break;
                    }
                    Progress = i;
                }
                if (_continue)
                {
                    primitive = false;
                    w += 1;
                    Progress = 0;
                }
                else
                {
                    primitive = true;
                }
            }
            OEString = UInt64String(w);
        }
        private void GaloisAlgorithm()
        {
            if (InputFile == "")
            {
                MessageBox.Show("Вкажіть шлях до вхідного файлу!");
                return;
            }
            if (OutputFile == "")
            {
                MessageBox.Show("Вкажіть шлях до вихідного файлу!");
                return;
            }
            if (OEString == "")
            {
                MessageBox.Show("Відсутній примітивний утворюючий елемент!");
                return;
            }
            if (VIString == "")
            {
                MessageBox.Show("Відсутній вектор ініціалізації!");
                return;
            }
            Buttons = false;
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerDone);
            worker.DoWork += new DoWorkEventHandler(GaloisAlgorithm);
            timer = System.Diagnostics.Stopwatch.StartNew();
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        private static UInt64[] GenerateMatrix(BigInteger f, UInt64 w, int size)
        {
            UInt64[] array = new UInt64[size];
            array[0] = w;
            for (int i = 1; i < size; i++)
            {
                BigInteger a = array[i - 1] << 1;
                if (a > 0xFFFFFFFFFFFFFFFF)
                    a = a ^ f;
                array[i] = (UInt64)a;
            }
            return array;
        }
        private void GaloisAlgorithm(object sender, DoWorkEventArgs e)
        {
            UInt64 OE = Convert.ToUInt64(OEString, 2);
            UInt64 VI = Convert.ToUInt64(VIString, 2);
            BigInteger polynomial = Polynomial.GetPolynom(CurrentPolynom);
            UInt64[] galoisMatrix = GenerateMatrix(polynomial, OE, length);
            byte[] inputBytes = File.ReadAllBytes(InputFile);
            UInt64 vector = VI;
            MaxProgress = inputBytes.Length;
            for (int loopI = 0; loopI < inputBytes.LongLength; loopI++)
            {
                UInt64 g = 0;
                for (int i = 0; i < sizeof(UInt64) * 8; i++)
                {
                    g ^= galoisMatrix[length - 1 - i] & galoisMatrix[length - 1 - i] * (vector & 0x1);
                    vector >>= 1;
                }
                vector = g;
                byte[] gBytes = BitConverter.GetBytes(vector);
                int count = AlgorithmIndex == 0 ? 8 : AlgorithmIndex == 1 ? 4 : 2;
                byte x = 0;
                for (int i = 0; i < count; i++)
                {
                    byte gByte = gBytes[i];
                    x ^= gByte;
                }
                inputBytes[loopI] ^= x;
                Progress = loopI;
            }
            File.WriteAllBytes(OutputFile, inputBytes);
        }
        private void WorkerDone(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            Time = timer.Elapsed.ToString();
            Buttons = true;
            MaxProgress = 1;
            Progress = 0;
        }
        private string UInt64String(UInt64 value)
        {
            int ulongsize = sizeof(UInt64) * 8;
            StringBuilder ret = new StringBuilder(ulongsize);

            for (int i = 0; i < ulongsize; i++)
            {
                ret.Insert(0, value & 0x1);
                value >>= 1;
            }

            return ret.ToString();
        }
        private void Refresh()
        {
            OEString = "";
            VIString = "";
            InputFile = "";
            OutputFile = "";
        }
        public void Dispose()
        {

        }
    }
}
