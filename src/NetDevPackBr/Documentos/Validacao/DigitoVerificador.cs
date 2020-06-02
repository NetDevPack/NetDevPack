using System.Collections.Generic;

namespace NetDevPackBr.Documentos.Validacao
{
    public class DigitoVerificador
    {
        private string _numero;
        private int _modulo = 11;
        private bool _somarAlgarismos;
        private bool _complementarDoModulo = true;
        private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();
        
        public DigitoVerificador(string numero)
        {
            _numero = numero;
        }

        public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multiplicadores.Clear();

            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
            {
                _multiplicadores.Add(i);
            }

            return this;
        }

        public DigitoVerificador ComMultiplicadores(params int[] multiplicadores)
        {
            _multiplicadores.Clear();

            foreach (var i in multiplicadores)
            {
                _multiplicadores.Add(i);
            }

            return this;
        }

        public DigitoVerificador Substituindo(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _substituicoes[i] = substituto;
            }

            return this;
        }

        public DigitoVerificador Modulo(int modulo)
        {
            _modulo = modulo;
            return this;
        }

        public DigitoVerificador SomandoAlgarismos()
        {
            _somarAlgarismos = true;
            return this;
        }

        public DigitoVerificador InvertendoMultiplicadores()
        {
            _multiplicadores.Reverse();
            return this;
        }

        public DigitoVerificador SemComplementarDoModulo()
        {
            _complementarDoModulo = false;
            return this;
        }

        public void AddDigito(string digito) => _numero = string.Concat(_numero, digito);

        public string CalculaDigito() => !(_numero.Length > 0) ? "" : ObterSomaDosDigitos();

        private string ObterSomaDosDigitos()
        {
            var soma = 0;
            for (int i = _numero.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_numero[i]) * _multiplicadores[m];
                soma += _somarAlgarismos ? SomaAlgarismos(produto) : produto;

                if (++m >= _multiplicadores.Count) m = 0;
            }

            var mod = (soma % _modulo);
            var resultado = _complementarDoModulo ? _modulo - mod : mod;

            return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
        }

        private static int SomaAlgarismos(int produto) => (produto / 10) + (produto % 10);

        private int ProximoMultiplicador(int multiplicadorAtual) => multiplicadorAtual;
    }
}