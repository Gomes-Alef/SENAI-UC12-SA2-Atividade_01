using System;

namespace Atividade
{
    // Classe-pai (abstração do "cliente" do mundo real)
    class Clientes
    {
        private string _nome;

        // Encapsulamento: o set valida o valor antes de guardar
        public string nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome não pode ficar em branco.");
                _nome = value.Trim();
            }
        }

        public string endereco { get; set; }

        // "#" no diagrama = protected: só a própria classe e as filhas alteram
        public float valor { get; protected set; }
        public float valor_imposto { get; protected set; }
        public float total { get; protected set; }

        // virtual: pode ser reescrito (override) nas classes filhas
        public virtual void Pagar_Imposto(float v)
        {
            if (v <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");

            this.valor = v;
            this.valor_imposto = this.valor * 10 / 100;   // 10% (pessoa física)
            this.total = this.valor + this.valor_imposto;
        }

        // Linha que será gravada no arquivo; cada filha acrescenta seus dados
        public virtual string ParaArquivo()
        {
            return nome + ";" + endereco + ";" + valor.ToString("F2") + ";" +
                   valor_imposto.ToString("F2") + ";" + total.ToString("F2");
        }
    }
}
