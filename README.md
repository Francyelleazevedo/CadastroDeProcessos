
# Cadastro de Processos

Este projeto é uma aplicação web em C# que permite cadastrar, listar, editar e excluir processos.

## Tecnologias Utilizadas

- .NET 8 MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5.1.0 para estilização
- FontAwesome para ícones
- Arquitetura limpa

## Configuração do Banco de Dados

1. Abra o **Package Manager Console** no Visual Studio.
2. Execute o comando para criar o banco de dados e aplicar as migrações:

    ```bash
    Add-Migration InitialCreate
    ```
    
    ```bash
    Update-Database
    ```

3. Use o arquivo `script.sql` fornecido para criação manual, se preferir.

## Como Executar o Projeto

1. Clone o repositório:
    ```bash
    git clone https://github.com/SEU_USUARIO/CadastroProcessos.git
    ```
2. Abra o projeto no Visual Studio.
3. Configure a **connection string** no arquivo `appsettings.json` para apontar para o seu banco de dados.
