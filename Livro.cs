using System.Text;
class Livro
{
    public string tituloLivro = "";
    public string autorLivro = "";
    public string statusNovo = "";
    public int anoPublicacao = 0;
    public int numPag = 0;
    public bool status = true;
    public Livro(string tituloLivro, string autorLivro, int anoPublicacao, int numPag, bool status)
    {
        try//Tratamento erro no arquivo
        {
            //Criação de um arquivo para armazenar os livros da biblioteca
            StreamWriter escritor = new StreamWriter("Lista-Livros.txt", true, Encoding.UTF8); 
            //Construtor do livro
            this.tituloLivro = tituloLivro;
            this.autorLivro = autorLivro;
            this.anoPublicacao = anoPublicacao;
            this.numPag = numPag;
            this.status = status;

            if (status == true)
            {
                statusNovo = "DISPONIVEL";
            }
            else
            {
                statusNovo = "EMPRESTADO";
            }
            //Adiciona o livro no arquivo
            escritor.Write($"Título: {tituloLivro} ; Autor: {autorLivro} ; Ano Publicação: {anoPublicacao} ; Número de páginas: {numPag} ; Status: {statusNovo}.\n");
            escritor.Close();
        }
        catch (Exception e) //Tratamento de erro no arquivo.
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public void AlteraStatus(string tituloLivro, Biblioteca biblioteca)
    {
        for (int i = 0; i < biblioteca.livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                if (biblioteca.livros[i].tituloLivro == tituloLivro && biblioteca.livros[i].status == true)
                {
                    status = false;
                    statusNovo = "EMPRESTADO";
                }
                else if (biblioteca.livros[i].tituloLivro == tituloLivro && biblioteca.livros[i].status == false)
                {
                    status = true;
                    statusNovo = "DISPONIVEL";
                }
            }
        }
    }
    public void ExibirInfos(string tituloLivro, Biblioteca biblioteca)
    {
        int posicao;
        for (int i = 0; i < biblioteca.livros.Length; i++)
        {
            if (biblioteca.livros[i] != null)
            {
                if (biblioteca.livros[i].tituloLivro == tituloLivro)
                {
                    posicao = i;
                    tituloLivro = biblioteca.livros[posicao].tituloLivro;
                    autorLivro = biblioteca.livros[posicao].autorLivro;
                    anoPublicacao = biblioteca.livros[posicao].anoPublicacao;
                    numPag = biblioteca.livros[posicao].numPag;
                    status = biblioteca.livros[posicao].status;

                    if (status == true)
                    {
                        statusNovo = "DISPONIVEL";
                    }
                    else
                    {
                        statusNovo = "EMPRESTADO";
                    }

                    Console.WriteLine($"****INFORMAÇÕES DO LIVRO****\n--------------------\nTitulo: {tituloLivro}\nAutor: {autorLivro} \nAno Publicação: {anoPublicacao} \nNúmero de Páginas: {numPag} \nStatus do livro: {statusNovo}\n--------------------\n");
                }
            }
        }
    }
}