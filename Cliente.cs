using System.Text;
class Cliente
{
    Biblioteca biblioteca = new Biblioteca();
    private string nomeCliente = "";
    private int numID = 0;
    public string[] listEmprest = new string[10];
    public string getNome()
    {
        return nomeCliente;
    }
    public int getNumID()
    {
        return numID;
    }
    public Cliente[] clientes = new Cliente[10];
    public void Construtor(string nomeCliente, int numID, string[] listEmprest)
    {
        try//Tratamento de erro no arquivo
        { 
            StreamWriter escritor = new StreamWriter("Lista-Clientes.txt", true, Encoding.UTF8);//Criação de um arquivo para armazenar os clientes da biblioteca

            this.nomeCliente = nomeCliente;
            this.numID = numID;
            this.listEmprest = listEmprest;

            escritor.Write($"Nome: {nomeCliente} ; ID: {numID};\n");
            escritor.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

    }

    int quantClientes = 0;
    public void InserirCliente(string nomeCliente, int idCliente, string[] listEmprest)
    {
        //Construtor de cliente..
        Cliente cliente = new Cliente();
        cliente.Construtor(nomeCliente, idCliente, listEmprest);
        clientes[quantClientes] = cliente;
        quantClientes++;
        Console.WriteLine("\n****CLIENTE CADASTRADO COM SUCESSO****");
    }
    int posicao = 0, posicaoLivro = 0;
    public void EmprestarLivro(string tituloLivro, int idCliente, Biblioteca biblioteca, Cliente cliente)
    {
        for (int i = 0; i < clientes.Length; i++)
        {
            if (clientes[i] != null)
            {
                if (clientes[i].numID == idCliente)
                {
                    posicao = i;
                    break;
                }
            }
        }
        for (int i = 0; i < biblioteca.livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                if (biblioteca.livros[i].tituloLivro == tituloLivro)
                {
                    posicaoLivro = i;
                    break;
                }
            }
        }
        biblioteca.livros[posicaoLivro].AlteraStatus(tituloLivro, biblioteca);
        for (int i = 0; i < cliente.clientes[posicao].listEmprest.Length; i++)
        {
            if (cliente.clientes[posicao].listEmprest[i] == null)
            {
                cliente.clientes[posicao].listEmprest[i] = tituloLivro;
                break;
            }
        }
        Console.WriteLine($"\n****O livro {tituloLivro} foi emprestado para o cliente com ID: {idCliente}****\n");
        try
        {
            string autorLivro, statusNovo;
            int anoPublicacao, numPag;
            autorLivro = biblioteca.livros[posicaoLivro].autorLivro;
            anoPublicacao = biblioteca.livros[posicaoLivro].anoPublicacao;
            numPag = biblioteca.livros[posicaoLivro].numPag;
            statusNovo = biblioteca.livros[posicaoLivro].statusNovo;

            //Chamada do arquivo para alterar o status do livro no arquivo
            string[] palavras;
            char[] separador = { ';' };
            string[] linhas = File.ReadAllLines("Lista-Livros.txt", Encoding.UTF8);
            for (int i = 0; i < linhas.Length; i++)
            {
                palavras = linhas[i].Split(separador);

                if (palavras[0].Trim() == "Título: " + tituloLivro)
                {
                    linhas[i] = $"Título: {tituloLivro} ; Autor: {autorLivro} ; Ano Publicação: {anoPublicacao} ; Número de páginas: {numPag} ; Status: {statusNovo}.\n";
                    break;
                }
            }
            File.WriteAllLines("Lista-Livros.txt", linhas, Encoding.UTF8);
        }
        catch (Exception e) //Tratamento de erro no arquivo
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public void DevolverLivro(string tituloLivro, int idCliente, int j, Biblioteca biblioteca, Cliente cliente)
    {
        for (int i = 0; i < listEmprest.Length; i++)
        {
            if (cliente.clientes[j].listEmprest[i] != null)
            {
                if (cliente.clientes[j].listEmprest[i] == tituloLivro)
                {
                    cliente.clientes[j].listEmprest[i] = null;
                }
            }
        }
        biblioteca.livros[posicao].AlteraStatus(tituloLivro, biblioteca);
        Console.WriteLine($"O livro {tituloLivro} que estava emprestado para o cliente com ID {idCliente} foi devolvido para a biblioteca.");
        Console.WriteLine("\n****O livro foi removido da lista do cliente****\n");
        //Chamada do arquivo para alterar o status do livro no arquivo
        try
        {
            string autorLivro, statusNovo;
            int anoPublicacao, numPag;
            autorLivro = biblioteca.livros[posicaoLivro].autorLivro;
            anoPublicacao = biblioteca.livros[posicaoLivro].anoPublicacao;
            numPag = biblioteca.livros[posicaoLivro].numPag;
            statusNovo = biblioteca.livros[posicaoLivro].statusNovo;

            string[] palavras;
            char[] separador = { ';' };
            string[] linhas = File.ReadAllLines("Lista-Livros.txt", Encoding.UTF8);
            for (int i = 0; i < linhas.Length; i++)
            {
                palavras = linhas[i].Split(separador);

                if (palavras[0].Trim() == "Título: " + tituloLivro)
                {
                    linhas[i] = $"Título: {tituloLivro} ; Autor: {autorLivro} ; Ano Publicação: {anoPublicacao} ; Número de páginas: {numPag} ; Status: {statusNovo}.\n";
                    break;
                }
            }
            File.WriteAllLines("Lista-Livros.txt", linhas, Encoding.UTF8);
        }
        catch (Exception e)//Tratamento de erro no arquivo
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public void ExibirInfosClie(int numID, Cliente cliente, Biblioteca biblioteca)
    {
        for (int i = 0; i < cliente.clientes.Length; i++)
        {
            if (cliente.clientes[i] != null)
            {
                if (cliente.clientes[i].numID == numID)
                {
                    posicao = i;
                }
            }
        }
        nomeCliente = cliente.clientes[posicao].nomeCliente;
        numID = cliente.clientes[posicao].numID;
        Console.WriteLine($"\n--------------------\n****INFORMAÇÕES DO CLIENTE****\n\nNome: {nomeCliente}\nID: {numID}\nLista de Livros EMPRESTADOS: ");
        foreach (string livros in listEmprest)//Busca todos os livros emprestados do cliente 
        {
            if (livros != null)
            {
                Console.WriteLine(livros);
            }
        }
    }
}