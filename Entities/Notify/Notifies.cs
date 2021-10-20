using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Notify
{
    public class Notifies
    {
        public Notifies()
        {
            Notitycoes = new List<Notifies>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notitycoes;

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarDocumentoString(string valor, string nomePropriedade)
        {

            if (!valor.Contains("Vazia"))
            {
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Documento já existente no sistema",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    mensagem = "O valor deve ser maior do que 0",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }
    }
}
