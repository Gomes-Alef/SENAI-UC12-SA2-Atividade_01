namespace Atividade
{
    // Herança: Pessoa_Fisica é um tipo de Clientes
    class Pessoa_Fisica : Clientes
    {
        public string cpf { get; set; }
        public string rg { get; set; }

        // Não reescreve Pagar_Imposto: usa os 10% herdados da classe-pai

        public override string ParaArquivo()
        {
            return "PF;" + cpf + ";" + rg + ";" + base.ParaArquivo();
        }
    }
}
