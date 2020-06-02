using System;
using NetDevPack.Domain;
using NetDevPack.Utilities;
using NetDevPackBr.Documentos.Validacao;

namespace NetDevPackBr.Documentos
{
    public class Cnpj
    {
        public string Numero { get; set; }

        public Cnpj(string numero)
        {
            Numero = numero.OnlyNumbers(numero);
            if (!EstaValido()) throw new DomainException("CNPJ Inválido");
        }

        public override string ToString() => SemMascara();

        public string ComMascara()
        {
            if (Numero == "")
                return "";

            const string pattern = @"{0:00\.000\.000\/0000\-00}";
            return string.Format(pattern, Convert.ToUInt64(Numero));
        }

        public string SemMascara() => Numero;

        public bool EstaValido() => new CnpjValidador(Numero).EstaValido();

        public bool Equals(Cnpj cnpj) => Numero == cnpj.SemMascara();
    }
}