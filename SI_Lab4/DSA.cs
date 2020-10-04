using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;

namespace SI_Lab4
{
    class DSA
    {
        public BigInteger p, q, g, h;
        public BigInteger x, y;
        public BigInteger r, s;

        public static Random generator = new Random();

        public DSA()
        {
            GetParameters();
            GetKey();
        }

        private void GetParameters()
        {
            do
            {
                q = RandomBigInteger(20);
            } while (!ProbablyPrime(q));

            do
            {
                p = RandomBigInteger(128);
            } while (!ProbablyPrime(p)|| (p - 1) % q != 0);

            do
            {
                h = generator.Next(2, Int32.MaxValue);
                g = BigInteger.ModPow(h, (p - 1) / q, p);
            } while (g == 1);
        }
        public static bool ProbablyPrime(BigInteger n)
        {
            return BigInteger.One.Equals(BigInteger.ModPow(new BigInteger(2), BigInteger.Subtract(n, BigInteger.One), n));
        }

        public static BigInteger RandomBigInteger(int numOfBytes)
        {
            byte[] bytes = new byte[numOfBytes+1];
            generator.NextBytes(bytes);
            if (bytes[numOfBytes-1] < 128)
            {
                bytes[numOfBytes-1]+= 128;
            }
            bytes[numOfBytes] = 0;

            return new BigInteger(bytes);
        }

        static BigInteger GetHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return new BigInteger(hash);
            }
        }

        private void GetKey()
        {
            var ran = new Random();
            x = ran.Next(0, (int)q);
            y = BigInteger.ModPow(g, x, p);
        }


        public void getSignature(String message)
        {
            var generator = new Random();
            var hash = GetHash(message);
            do
            {
                BigInteger k = generator.Next(0, (int)q);
                BigInteger k1 = BigInteger.ModPow(k, q - 2, q);
                r = BigInteger.ModPow(g, k, p) % q;
                s = ((k1) * (hash + x * r)) % q;
            } while (s == 0 || r == 0);
        }

        public bool check(String message, BigInteger getR, BigInteger getS)
        {
            if ((getR < 0) || (getR > q) || (getS < 0) || (getS > q)) return false;
            var hash = GetHash(message);
            BigInteger w = BigInteger.ModPow(s, q - 2, q);
            //BigInteger w = (s1) % q;
            BigInteger u1 = (hash * w) % q;
            BigInteger u2 = (r * w) % q;
            BigInteger v = ((BigInteger.ModPow(g, u1, p) * BigInteger.ModPow(y, u2, p)) % p) % q;
            return (v == r);
        }
    
    }
}
