using System.Collections.Generic;
using NetDevPack.Utilities;

namespace NetDevPackBr.Documentos.Validacao
{
    public class CpfValidador
    {
        private const int TamanhoCpf = 11;
        private readonly string _cpfTratado;

        public CpfValidador(string numero) => _cpfTratado = numero.OnlyNumbers(numero);

        public bool EstaValido()
        {
            if (!PossuiTamanhoValido()) return false;
            if (PossuiDigitosRepetidos()) return false;
            return PossuiDigitosValidos();
        }

        private bool PossuiTamanhoValido() => _cpfTratado.Length == TamanhoCpf;

        private bool PossuiDigitosRepetidos()
        {
            var cpfsInvalidos = new List<string>()
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return cpfsInvalidos.Contains(_cpfTratado);
        }

        private bool PossuiDigitosValidos()
        {
            var numero = _cpfTratado.Substring(0, TamanhoCpf - 2);

            var digitoVerificador = new DigitoVerificador(numero)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);

            var primeiroDigito = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(primeiroDigito);

            var segundoDigito = digitoVerificador.CalculaDigito();

            return string.Concat(primeiroDigito, segundoDigito) == _cpfTratado.Substring(TamanhoCpf - 2, 2);
        }
    }
}