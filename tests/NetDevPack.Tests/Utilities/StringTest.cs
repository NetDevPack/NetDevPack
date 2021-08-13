using FluentAssertions;
using NetDevPack.Utilities;
using Xunit;

namespace NetDevPack.Tests.Utilities
{

    public class StringTest
    {

        [Theory]
        [InlineData("my sensitive data", "m***************a")]
        public void Should_Truncate_Sensitive_Information(string data, string expected)
        {
            data.TruncateSensitiveInformation().Should().Be(expected);
        }


        [Theory]
        [InlineData("Em qual camada ficaria o envio de emails?", "em-qual-camada-ficaria-o-envio-de-emails")]
        [InlineData("Utilizando Refit para o consumo de API's", "utilizando-refit-para-o-consumo-de-api-s")]
        [InlineData("Processo rápido - CRUD", "processo-rapido-crud")]
        [InlineData(".net core 5.0 - pacotes", "net-core-5-0-pacotes")]
        [InlineData("Versão do AspNetCore do curso", "versao-do-aspnetcore-do-curso")]
        [InlineData(" Tipo de dado gerado pela configuração HasCloumnType<string> ", "tipo-de-dado-gerado-pela-configuracao-hascloumntype-string")]
        public void Should_Urlize(string content, string expected)
        {
            content.Urlize().Should().Be(expected);
        }

        [Theory]
        [InlineData("86456-221", "86456221")]
        [InlineData("864aoeuaoeu56-221", "86456221")]
        [InlineData("#@eee221oeu#@ aoeu a,34", "22134")]
        public void Should_Only_Get_Numbers(string content, string expected)
        {
            content.OnlyNumbers().Should().Be(expected);
        }
    }
}
