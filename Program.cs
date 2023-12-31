/*
Autor do Programa: Vinicius Ferreira Mello.
Data de construção: 24/08/2023  
Última atualização: 01/09/2023
*/
class Program
{
    //Funções responsáveis pelas opções do menu. Elas tratam as informações digitadas pelo usuário e poder enviá-las corretamente para as classes e métodos.
    static void AddLivro(Biblioteca biblioteca)
    {
        bool tem = false;
        do
        {
            try //Tratamento para ver se o arquivo existe
            {
                Console.WriteLine("--------------------\n****CADASTRO DE LIVRO****\n");
                tem = true;

                Console.WriteLine("Digite o nome do livro, ou -1 para voltar ao menu");
                string nomeLivro = Console.ReadLine();
                if (nomeLivro == "-1")
                {
                    tem = true;
                }
                string[] palavras;
                char[] separador = { ';' };

                string[] linhas = File.ReadAllLines("Lista-Livros.txt"); //Lê o arquivo de texto para verificar se esse livro já está cadastrado.
                for (int i = 0; i < linhas.Length; i++)
                {
                    if (linhas[i] != null)
                    {
                        palavras = linhas[i].Split(separador);

                        if (palavras[0].Trim() == "Título: " + nomeLivro)
                        {
                            tem = false;
                        }
                    }
                }
                if (!tem)
                {
                    Console.WriteLine("****\nEsse livro já existe na biblioteca.*****");
                    tem = false;
                }
                if (nomeLivro != "-1")
                {
                    if (tem)
                    {
                        Console.WriteLine("Digite o nome do autor do livro:");
                        string nomeAutor = Console.ReadLine();

                        Console.WriteLine("Digite o ano de publicação do livro:");
                        int anoPublicacao = int.Parse(Console.ReadLine());

                        Console.WriteLine("Digite a quantidade de páginas do livro:");
                        int quantPag = int.Parse(Console.ReadLine());

                        Console.WriteLine("**** LIVRO CADASTRADO COM SUCESSO****");
                        bool status = true;
                        nomeLivro = nomeLivro.ToUpper();
                        nomeAutor = nomeAutor.ToUpper();

                        biblioteca.InserirLivro(nomeLivro, nomeAutor, anoPublicacao, quantPag, status); ;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("O arquivo não existe.");
            }
        }while (!tem);

    }
    static void RemoverLivro(Biblioteca biblioteca)
    {
        Console.WriteLine("--------------------\n****REMOÇÃO DE LIVRO****\n");
        bool tem = false;
        Console.WriteLine("\nDigite o nome do livro que deseja remover da biblioteca, ou -1 para voltar ao menu");
        string NomeLivro = Console.ReadLine();
        if (NomeLivro == "-1")
        {
            tem = true;
        }
        for (int i = 0; i < biblioteca.livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == false)
                {
                    Console.WriteLine("\n****Esse livro está emprestado. Não é possível removê-lo****");
                    tem = true;
                    break;
                }
                if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == true)
                {
                    biblioteca.RemoverLivro(NomeLivro);
                    tem = true;
                    break;
                }
            }
        }
        if (!tem)
        {
            Console.WriteLine("****Livro não encontrado na biblioteca. Digite um livro que exista!****");
            NomeLivro = Console.ReadLine();
        }
    }
    static void EmprestarLivro(Biblioteca biblioteca, Cliente cliente)
    {
        bool tem = false;
        biblioteca.ExibirLivrosDisponiveis(biblioteca);

        Console.WriteLine("Digite o nome do livro que deseja Emprestar ou -1 para voltar ao menu\n");
        string NomeLivro = Console.ReadLine();
        if (NomeLivro == "-1")
        {
            tem = true;
        }
        NomeLivro = NomeLivro.ToUpper(); ;
        do
        {
            for (int i = 0; i < biblioteca.livros.Length; i++)
            {
                if (biblioteca.livros[i] != null)
                {
                    if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == true)
                    {
                        tem = true;
                        break;
                    }
                }
                if (!tem)
                {
                    if (biblioteca.livros[i] != null)
                    {
                        if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == false)
                        {
                            Console.WriteLine("****Esse livro já está emprestado. Digite outro****");
                            NomeLivro = Console.ReadLine();
                            NomeLivro = NomeLivro.ToUpper();
                            tem = true;
                            break;
                        }
                    }
                }
            }
            if (!tem)
            {
                Console.WriteLine("****Livro não encontrado na biblioteca. Digite um livro que exista!****");
                NomeLivro = Console.ReadLine();
                NomeLivro = NomeLivro.ToUpper();
            }
        } while (!tem);
        if (NomeLivro != "-1")
        {
            biblioteca.ExibirClientes(cliente);
            Console.WriteLine("Digite o ID do cliente que deseja emprestar esse livro:");
            try//Tratamento para entrada de ID do usuário.
            {
                int id = int.Parse(Console.ReadLine());
                tem = false;
                do
                {
                    for (int i = 0; i < cliente.clientes.Length; i++)
                    {
                        if (cliente.clientes[i] != null)
                        {
                            if (cliente.clientes[i].getNumID() == id)
                            {
                                cliente.clientes[i].EmprestarLivro(NomeLivro, id, biblioteca, cliente);
                                tem = true;
                                break;
                            }
                        }
                    }
                    if (!tem)
                    {
                        Console.WriteLine("****Não existe cliente com esse ID. Digite um ID válido.****");
                        id = int.Parse(Console.ReadLine());
                    }
                } while (!tem);
            }
            catch (FormatException)
            {
                Console.WriteLine("OPS, você digitou um caractere inválido. O cadastro de ID é apenas números.");
            }
        }
    }
    static void DevolverLivro(Biblioteca biblioteca, Cliente cliente)
    {
        bool tem = false;
        do
        {
            int idCliente;
            Console.WriteLine("Digite o nome do livro que deseja DEVOLVER, ou -1 para voltar ao menu.");
            string NomeLivro = Console.ReadLine();
            if (NomeLivro == "-1")
            {
                tem = true;
            }
            for (int i = 0; i < biblioteca.livros.Length; i++)
            {
                if (biblioteca.livros[i] != null)
                {
                    if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == true)
                    {
                        Console.WriteLine("****Esse livro não está emprestado. Digite outro****\n");
                        NomeLivro = Console.ReadLine();
                    }
                }
            }
            for (int j = 0; j < cliente.clientes.Length; j++)
            {
                if (cliente.clientes[j] != null)
                {
                    for (int i = 0; i < biblioteca.livros.Length; i++)
                    {
                        if (biblioteca.livros[i] != null)
                        {
                            if (biblioteca.livros[i].tituloLivro == NomeLivro && biblioteca.livros[i].status == false)
                            {
                                for (int cont = 0; cont < cliente.listEmprest.Length; cont++)
                                {
                                    if (cliente.clientes[j].listEmprest[cont] != null)
                                    {
                                        if (cliente.clientes[j].listEmprest[cont] == NomeLivro)
                                        {
                                            idCliente = cliente.clientes[j].getNumID();
                                            NomeLivro = NomeLivro.ToUpper();
                                            cliente.clientes[j].DevolverLivro(NomeLivro, idCliente, j, biblioteca, cliente);
                                            tem = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            if (!tem)
            {
                Console.WriteLine("****Livro não encontrado na biblioteca. Digite um livro que exista!****\n");
            }
        } while (!tem);
    }
    static void RegistrarCliente(Cliente cliente)
    {
        bool tem = true;
        string nomeCliente;
        Console.WriteLine("Digite o nome do cliente, ou -1 para voltar ao menu");
        nomeCliente = Console.ReadLine();

        if (nomeCliente == "-1")
        {
            tem = false;
        }
        if (tem)
        {
            bool idExiste = true;
            do
            {
                Console.WriteLine("Digite o ID do cliente:");
                try //Tratamento para entrada de ID do usuário e ver se arquivo existe
                {
                    int id = int.Parse(Console.ReadLine());

                    string[] palavras;
                    char[] separador = { ';' };
                    string[] linhas = File.ReadAllLines("Lista-Clientes.txt"); //Lê o arquivo de texto para verificar se existe cliente com o mesmo ID já cadastrado.
                    for (int i = 0; i < linhas.Length; i++)
                    {
                        if (linhas[i] != null)
                        {
                            palavras = linhas[i].Split(separador);

                            if (palavras[1].Trim() == "ID: " + id)
                            {
                                idExiste = false;
                            }
                        }
                    }
                    if (idExiste)
                    {
                        nomeCliente = nomeCliente.ToUpper();
                        string[] listaEmprest = new string[10];
                        cliente.InserirCliente(nomeCliente, id, listaEmprest);
                    }
                    if (!idExiste)
                    {
                        Console.WriteLine("****Este ID já existe.****\n");
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("OPS, você digitou um caractere inválido. O cadastro de ID é apenas números.");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Arquivo não encontrado.");
                }
            } while (!idExiste);
        }
    }
    static void RemoverCliente(Cliente cliente, Biblioteca biblioteca)
    {
        bool tem = false;
        do
        {
            int contNull = 0, posicao = 0;
            Console.WriteLine("\nDigite o ID do cliente que você deseja remover da lista de clientes:");
            try//Tratamento para entrada de ID do usuário.
            {
                int id = int.Parse(Console.ReadLine());
                for (int i = 0; i < cliente.clientes.Length; i++)
                {
                    if (cliente.clientes[i] != null)
                    {
                        if (cliente.clientes[i].getNumID() == id)
                        {
                            posicao = i;
                            tem = true;
                        }
                    }
                }
                if (!tem)
                {
                    Console.WriteLine("Não existe cliente com esse ID. Digite um ID válido.\n");
                }
                if (tem)
                {
                    for (int j = 0; j < cliente.listEmprest.Length; j++)
                    {
                        if (cliente.clientes[posicao].listEmprest[j] == null)
                        {
                            contNull++;
                        }
                    }
                    if (contNull != cliente.clientes[posicao].listEmprest.Length)
                    {
                        Console.WriteLine("\nNão foi possível remover esse cliente do sistema pois existem livros emprestados para ele.");
                        tem = false;
                    }
                    if (contNull == cliente.clientes[posicao].listEmprest.Length)
                    {
                        biblioteca.RemoverCliente(id, cliente);
                        tem = true;
                    }
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("OPS, você digitou um caractere inválido. O cadastro de ID é apenas números.");
            }
        } while (!tem);

    }
    static void ExibirInfosLivros(Biblioteca biblioteca)
    {
        biblioteca.ExibirLivros(biblioteca);
        bool tem = false;
        do
        {
            Console.WriteLine("\n****Digite o nome do livro que deseja informações, ou -1 para voltar ao menu.****\n");
            string NomeLivro = Console.ReadLine();
            NomeLivro = NomeLivro.ToUpper();
            if (NomeLivro == "-1")
            {
                tem = true;
            }
            for (int i = 0; i < biblioteca.livros.Length; i++)
            {
                if (biblioteca.livros[i] != null)
                {
                    if (biblioteca.livros[i].tituloLivro == NomeLivro)
                    {

                        biblioteca.livros[i].ExibirInfos(NomeLivro, biblioteca);
                        tem = true;
                        break;
                    }
                }
            }
            if (!tem)
            {
                Console.WriteLine("\n*****Livro não encontrado no sistema! Digite outro.*****");
                NomeLivro = Console.ReadLine();
            }
        } while (!tem);
    }
    static void ExibirInfosClientes(Cliente cliente, Biblioteca biblioteca)
    {
        biblioteca.ExibirClientes2(cliente);
        bool tem = false;
        do
        {
            try//Tratamento para entrada de ID do usuário.
            {
                Console.WriteLine("\n*****Digite o ID do cliente que deseja informações, ou -1 para voltar ao menu.****\n");
                int id = int.Parse(Console.ReadLine());
                if (id == (-1))
                {
                    tem = true;
                }

                for (int i = 0; i < cliente.clientes.Length; i++)
                {
                    if (cliente.clientes[i] != null)
                    {
                        if (cliente.clientes[i].getNumID() == id)
                        {
                            cliente.clientes[i].ExibirInfosClie(id, cliente, biblioteca);
                            tem = true;
                            break;
                        }
                    }
                }
                if (!tem)
                {
                    Console.WriteLine("\n****ID do cliente não encontrado no sistema!.****");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("OPS, você digitou um caractere inválido. O cadastro de ID é apenas números.");
            }
        } while (!tem);
    }
    static void Main(string[] args)
    {
        Biblioteca biblioteca = new Biblioteca();
        Cliente cliente = new Cliente();

        Console.WriteLine("\n****BIBLIOTECA MELLO****\n");
        while (true)
        {//Menu inicial do programa
            Console.WriteLine("--------------------\nMENU DE OPÇÕES:\n--------------------\n1) Adicionar um livro na biblioteca\n2) Remover livro da biblioteca\n3) Emprestar livro\n4) Devolver livro\n5) Registrar clientes\n6) Remover clientes\n7) Exibir informações sobre livro\n8) Exibir informações sobre cliente\n0) Sair\n--------------------\n");
            try//Tratamento entrada de caractere do usuário.
            {
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        AddLivro(biblioteca);
                        break;

                    case 2:
                        RemoverLivro(biblioteca);

                        break;
                    case 3:
                        EmprestarLivro(biblioteca, cliente);
                        break;

                    case 4:
                        DevolverLivro(biblioteca, cliente);
                        break;

                    case 5:
                        RegistrarCliente(cliente);
                        break;

                    case 6:
                        RemoverCliente(cliente, biblioteca);
                        break;

                    case 7:
                        ExibirInfosLivros(biblioteca);
                        break;
                    case 8:
                        ExibirInfosClientes(cliente, biblioteca);

                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("\n****Digite uma opção válida:****\n");
                        break;
                }
                if (opcao == 0)
                {
                    Console.WriteLine("\n****PROGRAMA FINALIZADO****");
                    break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("OPS, você digitou um caractere inválido. Nosso MENU trabalha apenas com números.");
            }
        }
    }
}