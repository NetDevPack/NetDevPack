using System.Collections.Generic;
using NetDevPack.Utilities;

namespace NetDevPackBr.Documentos.Validacao
{
    public class CnpjValidador
    {
        private const int TamanhoCnpj = 14;
        private readonly string _cpnjTratado;

        public CnpjValidador(string cnpj) => _cpnjTratado = cnpj.OnlyNumbers(cnpj);

        public bool EstaValido()
        {
            if (!PossuiTamanhoValido()) return false;
            if (PossuiDigitosRepetidos()) return false;
            return PossuiDigitoVerificadorValido();
        }

        private bool PossuiTamanhoValido() => _cpnjTratado.Length == TamanhoCnpj;

        private bool PossuiDigitosRepetidos()
        {
            var cnpjInvalidos = new List<string>
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            return cnpjInvalidos.Contains(_cpnjTratado);
        }

        private bool PossuiDigitoVerificadorValido()
        {
            var numero = _cpnjTratado.Substring(0, TamanhoCnpj - 2);

            var digitoVerificador = new DigitoVerificador(numero)
                .ComMultiplicadoresDeAte(2, 9)
                .Substituindo("0", 10, 11);

            var primeiroDigito = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(primeiroDigito);

            var segundoDigito = digitoVerificador.CalculaDigito();

            return string.Concat(primeiroDigito, segundoDigito) == _cpnjTratado.Substring(TamanhoCnpj - 2, 2);
        }
    }
}