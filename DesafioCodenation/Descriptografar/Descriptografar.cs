using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DesafioCodenation.Descriptografar
{
    public class Criptografia
    {
        private static string[] _alfabeto = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        public static string mensagem = "";

        /// <summary>
        /// Função responsavel por descriptografar a mensagem
        /// </summary>
        /// <param name="textoCriptografado">mensagem a ser descriptografada</param>
        /// <param name="numeroCasas">quantidade de casas consideradas para descriptografia</param>
        /// <returns></returns>
        public static string descriptogrfar(string textoCriptografado, int numeroCasas)
        {

            for (int i = 0; i < textoCriptografado.Length; i++)
            {
                //Obtem o elemento em cada posição
                char letra = textoCriptografado.ElementAt(i);

                //Verifica se é uma letra
                if (char.IsLetter(letra))
                {
                    for (int j = 0; j < _alfabeto.Length; j++)
                    {
                        //Verifica se a letra consta no vetor e sa posição é maior ou igual a três
                        if (_alfabeto[j].Contains(letra) && j > 2)
                        {
                            //Adiciona a letra da posição de acordo com o numero de casas
                            mensagem += _alfabeto[j - numeroCasas].ToString();
                        }
                        else if (_alfabeto[j].Contains(letra) && j < 3)//se contem e menor que 3 adiciona a letra com a posição ja pre-definida
                        {
                            ObtemLetraComBaseNaPosicaoExpecifica(j);
                        }
                    }
                }
                else// se não adiciona a mensagem (espaço, pontos e numeros)
                {
                    mensagem += letra;
                }

            }

            return mensagem;
        }

        private static void ObtemLetraComBaseNaPosicaoExpecifica(int j)
        {
            if (j == 0)
            {
                mensagem += "x";
            }
            else if (j == 1)
            {
                mensagem += "y";
            }
            else if (j == 2)
            {
                mensagem += "z";
            }
        }

        /// <summary>
        /// Gera resumo criptografico da mensagem
        /// </summary>
        /// <param name="mensagem">A mensagem a ser resumida</param>
        /// <returns></returns>
        public static string GetSHA1(string mensagem)
        {
            string resumo = "";

            //Utiliza o Unicode para transforma os dados em array de bytes
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(mensagem);

            //Utilizo a classe SHA1Managed para calcular o valor do hash  para a matriz de bytes especificada
            SHA1Managed SHhash = new SHA1Managed();
            HashValue = SHhash.ComputeHash(MessageBytes);

            //faz o for para montar a strinf criptografada
            foreach (byte b in HashValue)
            {

                resumo += String.Format("{0:x2}", b);

            }

            return resumo;
        }
    }
}
