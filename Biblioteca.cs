class Biblioteca
{
    public Livro[] livros = new Livro[100];
    int quantLivros = 0;
    public void InserirLivro(string tituloLivro, string autorLivro, int anoPublicacao, int numPag, bool status)
    {
        //Insere cada livro em um vetor de livros.
        Livro livro = new Livro(tituloLivro, autorLivro, anoPublicacao, numPag, status);
        livros[quantLivros] = livro;
        quantLivros++;
    }
    public void RemoverLivro(string tituloLivro)
    {
        for (int i = 0; i < livros.Length; i++)
        {
            if (livros[i] != null)
            {
                if (livros[i].tituloLivro == tituloLivro)
                {
                    livros[i] = null;
                }
            }
        }
        Console.WriteLine("\n****LIVRO REMOVIDO DA BIBLIOTECA****\n");
        //Abrindo o arquivo para remover o livro dele.
        try
        {
            string[] palavras;
            char[] separador = { ';' };
            string[] linhas = File.ReadAllLines("Livros.txt");
            for (int i = 0; i < linhas.Length; i++)
            {
                palavras = linhas[i].Split(separador);

                if (palavras[0].Trim() == "Título: " + tituloLivro)
                {
                    linhas[i] = null;
                    break;
                }
            }
            File.WriteAllLines("Livros.txt", linhas);
        }
        catch (Exception e)//Tratamento de erro no arquivo
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public Cliente[] clientes = new Cliente[100];
    int quantClientes = 0;
    public void AdicionarCliente(string nomeCliente, int numID, string[] listEmprest)
    {
        //Inserir cliente em um vetor de clientes
        Cliente cliente = new Cliente();
        cliente.Construtor(nomeCliente, numID, listEmprest);
        clientes[quantClientes] = cliente;
        quantClientes++;
    }
    public void RemoverCliente(int numID, Cliente cliente)
    {
        for (int i = 0; i < cliente.clientes.Length; i++)
        {
            if (cliente.clientes[i] != null)
            {
                if (cliente.clientes[i].numID == numID)
                {
                    cliente.clientes[i] = null;
                }
            }
        }
        Console.WriteLine("****CLIENTE REMOVIDO DA BIBLIOTECA****");
        //Abrindo arquivo para remover o ciente dele.
        try
        {
            string[] palavras;
            char[] separador = { ';' };
            string[] linhas = File.ReadAllLines("Clientes.txt");
            for (int i = 0; i < linhas.Length; i++)
            {
                palavras = linhas[i].Split(separador);

                if (palavras[0].Trim() == "Nome: " + numID)
                {
                    linhas[i] = null;
                    break;
                }
            }
            File.WriteAllLines("Clientes.txt", linhas);
        }
        catch (Exception e) //Tratamento de erro do arquivo.
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public void ExibirLivrosDisponiveis(Biblioteca biblioteca)
    {
        //Exibição apenas dos livros que não estão emprestados.
        Console.WriteLine("--------------------\n****LIVROS DISPONÍVEIS****\n");
        for (int i = 0; i < livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                if (biblioteca.livros[i].status == true)
                {
                    Console.WriteLine(biblioteca.livros[i].tituloLivro);
                }
            }
        }
    }
    public void ExibirLivros(Biblioteca biblioteca)
    {
        //Exibição de todos livros.
        Console.WriteLine("--------------------\n****LIVROS DA BIBLIOTECA:****\n");
        for (int i = 0; i < livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                Console.WriteLine(biblioteca.livros[i].tituloLivro);
            }
        }
    }
    public void ExibirClientes(Cliente cliente)
    {
        //Informações completas das informações de todos clientes
        Console.WriteLine("--------------------\n****LISTA DE CLIENTES****\n");
        for (int i = 0; i < cliente.clientes.Length; i++)
        {
            if (cliente.clientes[i] != null)
            {
                Console.WriteLine($"Nome: {cliente.clientes[i].nomeCliente}\nID: {cliente.clientes[i].numID}\n----------\n");
            }
        }
    }
    public void ExibirClientes2(Cliente cliente)
    {
        //Exibição do número do ID de todos clientes (Para exibição durante o procedimento de emprestar livro)
        Console.WriteLine("--------------------\n****LISTA DE CLIENTES****\n");
        for (int i = 0; i < cliente.clientes.Length; i++)
        {
            if (cliente.clientes[i] != null)
            {
                Console.WriteLine($"ID: {cliente.clientes[i].numID}\n");
            }
        }
    }
}