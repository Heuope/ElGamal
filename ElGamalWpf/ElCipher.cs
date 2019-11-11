using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElGamalWpf
{
    class ElCipher
    {
        private readonly string _saveFilePath;
        private readonly string _loadFilePath;

        public ElCipher(string loadFilePath, string saveFilePath)
        {
            _loadFilePath = loadFilePath;
            _saveFilePath = saveFilePath;
        }

        public bool TryCipher(BigInteger p, BigInteger x, BigInteger k, BigInteger g)
        {
            if (string.IsNullOrEmpty(_saveFilePath) || string.IsNullOrEmpty(_loadFilePath))
                return false;
            if (!Ferma(p))
                return false;
            if (x >= p || k >= p || x < 1 || k < 1)
                return false;
            if (Gcd(k, p - 1) != 1)
                return false;
            if (FastPow(g, (p - 1) / 2, p) == 1)
                return false;
            if (FastPow(g, (p - 1) / 3, p) == 1)
                return false;

            Cipher(p, x, k, g);
            return true;
        }

        public bool TryDeCipher(BigInteger p, BigInteger x)
        {
            if (string.IsNullOrEmpty(_saveFilePath) || string.IsNullOrEmpty(_loadFilePath))
                return false;
            if (!Ferma(p))
                return false;
            if (x >= p || x < 1)
                return false;

            DeCipher(p, x);
            return true;
        }

        private BigInteger FastPow(BigInteger number, BigInteger power, BigInteger modul)
        {
            BigInteger x = 1;
            while (power != 0)
            {
                while (power % 2 == 0)
                {
                    power /= 2;
                    number = number * number % modul;
                }
                power--;
                x = (x * number) % modul;
            }
            return x;
        }

        private BigInteger Gcd(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return a;
            return Gcd(b, a % b);
        }

        private bool Ferma(BigInteger x)
        {
            if (x == 2)
                return true;

            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                BigInteger a = (rand.Next() % (x - 2)) + 2;
                if (Gcd(a, x) != 1)
                    return false;
                if (FastPow(a, x - 1, x) != 1)
                    return false;
            }
            return true;
        }

        private void DeCipher(BigInteger p, BigInteger x)
        {
            var initFile = System.IO.File.ReadAllBytes(_loadFilePath);
            var resFile = new List<byte>();

            for (var i = 0; i < initFile.Length; i += 2)
                Console.WriteLine((FastPow(initFile[i], p - 1 - x, p) * initFile[i + 1]) % p);

            System.IO.File.WriteAllBytes(_saveFilePath, resFile.ToArray());
        }

        private void Cipher(BigInteger p, BigInteger x, BigInteger k, BigInteger g)
        {
            

            for (int i = 2; i < p; i++)
                if (Gcd(i, p - 1) == 1)
                    Console.Write($"{i} ");

            var initFile = System.IO.File.ReadAllBytes(_loadFilePath);
            var resFile = new List<BigInteger>();

            var y = FastPow(g, x, p);
            var a = FastPow(g, k, p);

            foreach (var m in initFile)
            {
                resFile.Add(a);
                resFile.Add((FastPow(y, k, p) * m) % p);
            }

            //System.IO.File.WriteAllBytes(_saveFilePath, resFile.ToArray());
        }
    }
}
