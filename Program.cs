using System;
using System.Globalization;
using System.IO;

namespace Atividade
{
    class Program
    {
        const string ARQUIVO = "clientes.txt";

        static void Main(string[] args)
        {
            // Moeda em R$ e vírgula como separador decimal
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            string opcao = "";
            while (opcao != "0")
            {
                Console.WriteLine();
                Console.WriteLine("===== CONTROLE DE CLIENTES - ClientLab =====");
                Console.WriteLine("1 - Cadastrar cliente");
                Console.WriteLine("2 - Listar clientes salvos");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    CadastrarCliente();
                else if (opcao == "2")
                    ListarClientes();
                else if (opcao != "0")
                    Console.WriteLine("Opção inválida! Digite 1, 2 ou 0.");
            }
            Console.WriteLine("Sistema encerrado.");
        }

        static void CadastrarCliente()
        {
            string var_nome = LerTexto("Informar Nome:");
            string var_endereco = LerTexto("Informar Endereço:");
            string var_tipo = LerTipo();

            if (var_tipo == "f")
            {
                // --- Pessoa Física ---
                Pessoa_Fisica pf = new Pessoa_Fisica();
                pf.nome = var_nome;
                pf.endereco = var_endereco;
                pf.cpf = LerDocumento("Informar CPF (11 dígitos):", 11);
                pf.rg = LerTexto("Informar RG:");
                pf.Pagar_Imposto(LerValor());

                Console.WriteLine("-------- Pessoa Física ---------");
                Console.WriteLine("Nome ..........: " + pf.nome);
                Console.WriteLine("Endereço ......: " + pf.endereco);
                Console.WriteLine("CPF ...........: " + pf.cpf);
                Console.WriteLine("RG ............: " + pf.rg);
                MostrarValores(pf);
                Salvar(pf);
            }
            else
            {
                // --- Pessoa Jurídica ---
                Pessoa_Juridica pj = new Pessoa_Juridica();
                pj.nome = var_nome;
                pj.endereco = var_endereco;
                pj.cnpj = LerDocumento("Informar CNPJ (14 dígitos):", 14);
                pj.ie = LerTexto("Informar IE:");
                pj.Pagar_Imposto(LerValor());

                Console.WriteLine("-------- Pessoa Jurídica ---------");
                Console.WriteLine("Nome ..........: " + pj.nome);
                Console.WriteLine("Endereço ......: " + pj.endereco);
                Console.WriteLine("CNPJ ..........: " + pj.cnpj);
                Console.WriteLine("IE ............: " + pj.ie);
                MostrarValores(pj);
                Salvar(pj);
            }
        }

        // Recebe qualquer Clientes (PF ou PJ): polimorfismo
        static void MostrarValores(Clientes c)
        {
            Console.WriteLine("Valor de Compra: " + c.valor.ToString("C"));
            Console.WriteLine("Imposto .......: " + c.valor_imposto.ToString("C"));
            Console.WriteLine("Total a Pagar .: " + c.total.ToString("C"));
        }

        static void Salvar(Clientes c)
        {
            File.AppendAllText(ARQUIVO, c.ParaArquivo() + Environment.NewLine);
            Console.WriteLine("Cliente salvo em " + ARQUIVO);
        }

        static void ListarClientes()
        {
            if (!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Nenhum cliente cadastrado ainda.");
                return;
            }
            Console.WriteLine("-------- Clientes salvos ---------");
            Console.WriteLine("TIPO;DOCUMENTO;RG/IE;NOME;ENDEREÇO;VALOR;IMPOSTO;TOTAL");
            foreach (string linha in File.ReadAllLines(ARQUIVO))
                Console.WriteLine(linha);
        }

        // ---------- Leitura com tratamento de valores inválidos ----------

        static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                string texto = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(texto) && !texto.Contains(";"))
                    return texto.Trim();
                Console.WriteLine("Valor inválido! O campo não pode ficar vazio nem conter ';'.");
            }
        }

        static string LerTipo()
        {
            while (true)
            {
                Console.WriteLine("Pessoa Física (f) ou Jurídica (j)?");
                string tipo = (Console.ReadLine() ?? "").Trim().ToLower();
                if (tipo == "f" || tipo == "j")
                    return tipo;
                Console.WriteLine("Opção inválida! Digite f ou j.");
            }
        }

        // Aceita com ou sem pontuação (123.456.789-00) e confere a quantidade de dígitos
        static string LerDocumento(string mensagem, int qtdDigitos)
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                string entrada = Console.ReadLine() ?? "";
                string digitos = "";
                foreach (char ch in entrada)
                    if (char.IsDigit(ch)) digitos += ch;

                if (digitos.Length == qtdDigitos)
                    return digitos;
                Console.WriteLine("Documento inválido! Deve ter " + qtdDigitos + " dígitos.");
            }
        }

        static float LerValor()
        {
            while (true)
            {
                Console.WriteLine("Informar Valor de Compra:");
                string entrada = Console.ReadLine();
                float valor;
                if (float.TryParse(entrada, out valor) && valor > 0)
                    return valor;
                Console.WriteLine("Valor inválido! Digite um número maior que zero (ex.: 150,50).");
            }
        }
    }
}
