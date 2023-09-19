![Logo Cubos](https://cubos.io/cubos-brand.bbceae54.svg)


# Cubos - Desafio Pessoa Backend Junior

De acordo com o desafio proposto, foi construída uma API que simula uma aplicação financeira real, cobrindo a criação de contas, cartões e realização de transações.


# Sumário
1. <a href="#Tecnologias-Utilizadas">Tecnologias Utilizadas</a>
2. <a href="#Configurando-o-Projeto">Configurando o Projeto</a>
5. <a href="#Inicializando">Inicializando</a>
6. <a href="#Gerando-e-Implementando-Migrations-(Prisma)">Gerando e Implementando Migrations (Prisma)</a>
7. <a href="#Rodando-Testes">Rodando Testes</a>
8. <a href="#CI/CD">CI/CD</a>
9. <a href="#API-Endpoints">API Endpoints</a>
10. <a href="#Autor">Autor</a>


## Tecnologias Utilizadas

- [C#](https://dotnet.microsoft.com/pt-br/languages/csharp)
- [.NET](https://dotnet.microsoft.com/pt-br/)
- [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
- [Postgres](https://www.postgresql.org/)
- [Xunit](https://xunit.net/)


## Configurando o Projeto

Setar variáveis de ambiente de acordo. Os exemplos das variáveis utilizadas no projeto podem ser encontradas no arquivo:

```./fintech-challenge/appsettings.example.json```

|        Variável      |             Notas             |
| -------------------- | ------------------------------|
|     `DatabaseUrl`    |         Nome do Banco         |
|      `SecretKey`     |      Secret - Token Usuário   |

## Inicializando

- Clonar o repositório: `git@github.com:vianagustavo/fintech-challenge.git`
- Buildar o projeto: `dotnet build ./fintech-challenge/fintech-challenge.sln --configuration Release --no-restore`
- Executar aplicação: `dotnet run --project fintech-challenge`

## Gerando e Implementando Migrations (Entity Framework)

Para rodar as migrations existentes no projeto e configurar um banco de dados:

```
$ dotnet ef database update

```

## Rodando Testes

Com intuito de relizar testes automatizados, foram realizados testes que estão disponíveis para todos os endpoints da aplicação


```
# Rodando os testes
$ dotnet test ./fintech-challenge.Tests

```

## CI/CD

Foram utilizados os conceitos de CI/CD, através do GitHub Actions, sempre que for feito um push ou pull-request para a branch main, adotando boas práticas de desenvolvimento e automação da implantação da nossa aplicação.

Os principais steps envolvem o build do projeto e a realização dos testes para cada pipeline

O workflow completo pode ser encontrado em: 

``` .github/workflows/deploy.yml ```

## API Endpoints

Os endpoints acessíveis apenas à `Pessoa` pode ser acessado através de autenticação Bearer e o token necessário pode ser gerado através da rota de login

Utilize o header `Authentication: Bearer {token}` para autenticar as rotas protegidas

|  Verbo   |                    Endpoint                     |                 Descrição                  |     Acessível à:      |
| :------- | :---------------------------------------------: | :----------------------------------------: | :-------------------: |
| `POST`   |                     `/people`                   |           Criação de uma pessoa            |       ---------       |
| `POST`   |                   `/people/login`               |          Autenticação de pessoa            |       ---------       |
| `POST`   |          `/people/:peopleId/accounts`           |           Criação de uma conta             |        Pessoa         |
| `GET`    |          `/people/:peopleId/accounts`           |       Listagem de contas de uma pessoa     |        Pessoa         |
| `POST`   |          `/accounts/:accountId/cards`           |            Criação de um cartão            |        Pessoa         |
| `GET`    |          `/accounts/:accountId/cards`           |       Listagem de cartões de uma conta     |        Pessoa         |
| `GET`    |           `/people/:peopleId/cards`             |      Listagem dos cartões de uma pessoa    |        Pessoa         |


## Autor

- **Gustavo Ferreira Viana**