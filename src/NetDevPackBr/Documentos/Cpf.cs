using System;
using NetDevPack.Domain;
using NetDevPack.Utilities;
using NetDevPackBr.Documentos.Validacao;

namespace NetDevPackBr.Documentos
{
    public class Cpf
    {
        public string Numero { get; set; }

        public Cpf(string numero)
        {
            Numero = numero.OnlyNumbers(numero);
            if (!EstaValido()) throw new DomainException("CPF Inválido");
        }

        public override string ToString() => SemMascara();

        public string ComMascara()
        {
            if (Numero == "")
                return "";

            const string pattern = @"{0:000\.000\.000\-00}";
            return string.Format(pattern, Convert.ToUInt64(Numero));
        }

        public string SemMascara() => Numero;

        public bool EstaValido() => new CpfValidador(Numero).EstaValido();

        public bool Equals(Cpf cpf) => Numero == cpf.SemMascara();
    }
}