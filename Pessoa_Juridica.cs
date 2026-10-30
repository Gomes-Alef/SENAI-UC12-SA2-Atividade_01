using System;

namespace Atividade
{
    // Herança: Pessoa_Juridica é um tipo de Clientes
    class Pessoa_Juridica : Clientes
    {
        public string cnpj { get; set; }
        public string ie { get; set; }

        // Polimorfismo: mesmo método da classe-pai, com regra diferente (20%)
        public override void Pagar_Imposto(float v)
        {
            if (v <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");

            this.valor = v;
            this.valor_imposto = this.valor * 20 / 100;
            this.total = this.valor + this.valor_imposto;
        }

        public override string ParaArquivo()
        {
            return "PJ;" + cnpj + ";" + ie + ";" + base.ParaArquivo();
        }
    }
}
