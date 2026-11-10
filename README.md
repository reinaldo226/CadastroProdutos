# Cadastro de Produtos

Aplicação desktop Windows Forms para cadastro e gerenciamento de produtos, desenvolvida em C# com .NET Framework 4.8 e SQLite.

## Funcionalidades

- Inclusão de produtos.
- Edição de produtos existentes.
- Exclusão de produtos.
- Consulta por categoria.
- Exibição dos produtos em uma tabela.
- Validação de descrição, preço e estoque.
- Cálculo do valor total dos itens em estoque.
- Destaque visual para produtos sem estoque.
- Criação automática do banco de dados e da tabela na primeira execução.

## Tecnologias utilizadas

- C#
- .NET Framework 4.8
- Windows Forms
- SQLite
- `System.Data.SQLite`
- Visual Studio

## Estrutura do projeto

```text
CadastroProdutos/
├── App.config
├── CadastroProdutos.csproj
├── Form1.cs
├── Form1.Designer.cs
├── packages.config
├── Program.cs
└── Properties/
```

### Principais arquivos

- **`Program.cs`**: ponto de entrada da aplicação.
- **`Form1.cs`**: interface, eventos, validações e operações de cadastro.
- **`Form1.Designer.cs`**: controles e layout da tela Windows Forms.
- **`Banco`**: classe estática responsável pela conexão e operações com o SQLite. Ela está definida no final de `Form1.cs`.
- **`App.config`**: configuração do runtime do .NET Framework.
- **`packages.config`**: dependências NuGet do projeto.

## Banco de dados

O arquivo SQLite é criado automaticamente no diretório de execução da aplicação:

```text
produtos.db
```

A conexão utiliza o seguinte formato:

```text
Data Source=<diretório da aplicação>\produtos.db;Version=3;
```

Na primeira execução, a tabela `Produtos` é criada automaticamente:

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `INTEGER` | Chave primária autoincremental |
| `Descricao` | `TEXT` | Descrição obrigatória do produto |
| `Preco` | `REAL` | Preço obrigatório |
| `Estoque` | `INTEGER` | Quantidade em estoque, iniciada em zero |
| `Categoria` | `TEXT` | Categoria opcional |

As operações de inserção, atualização, exclusão e consulta usam parâmetros SQL para evitar a concatenação direta de valores na instrução SQL.

## Pré-requisitos

- Windows.
- Visual Studio com desenvolvimento para desktop .NET instalado.
- .NET Framework 4.8 Developer Pack.
- NuGet habilitado para restaurar pacotes.

## Dependências NuGet

O projeto declara os seguintes pacotes:

- `Stub.System.Data.SQLite.Core.NetFramework` `1.0.119.0`
- `System.Data.SQLite` `2.0.4`
- `System.Data.SQLite.Core` `1.0.119.0`

## Como executar

1. Clone ou copie o projeto para o computador.
2. Abra a solução ou o arquivo `CadastroProdutos.csproj` no Visual Studio.
3. Restaure os pacotes NuGet:
   - clique com o botão direito na solução;
   - selecione **Restaurar Pacotes NuGet**.
4. Selecione a configuração `Debug` ou `Release`.
5. Compile o projeto com **Build > Build Solution**.
6. Execute com `F5` ou `Ctrl + F5`.

O banco `produtos.db` será criado automaticamente quando a aplicação iniciar, caso ainda não exista.

## Como usar

### Cadastrar um produto

1. Preencha a descrição.
2. Informe um preço maior ou igual a zero.
3. Informe uma quantidade inteira maior ou igual a zero.
4. Opcionalmente, informe uma categoria.
5. Clique em **Salvar**.

### Editar um produto

1. Selecione uma linha na tabela.
2. Altere os dados desejados.
3. Clique em **Salvar**.

### Excluir um produto

1. Selecione o produto na tabela.
2. Clique em **Excluir**.
3. Confirme a operação.

### Buscar por categoria

1. Digite parte ou toda a categoria no campo **Buscar categoria**.
2. Clique em **Buscar**.
3. Para exibir todos os produtos novamente, limpe o campo e clique em **Buscar**.

## Solução de problemas

### Erro ao carregar `e_sqlite3`

Se ocorrer uma exceção semelhante a:

```text
System.DllNotFoundException: Não é possível carregar a DLL 'e_sqlite3'
```

isso normalmente indica que a DLL nativa do SQLite não foi copiada para a saída da compilação, que a arquitetura da DLL não corresponde à arquitetura do processo ou que há conflito entre os pacotes SQLite instalados.

Verifique os seguintes pontos:

1. Restaure os pacotes NuGet.
2. Exclua as pastas `bin` e `obj` e compile novamente.
3. Confirme se a DLL nativa esperada está presente no diretório de saída.
4. Use uma arquitetura compatível entre projeto, DLL e processo (`x86`, `x64` ou `Any CPU`).
5. Evite misturar provedores SQLite diferentes sem necessidade. O código atual usa o namespace `System.Data.SQLite`.
6. Confira se a versão do pacote e o arquivo nativo correspondem ao framework utilizado.

A mensagem `e_sqlite3` indica um problema de carregamento da biblioteca nativa, não um problema no SQL de criação da tabela.

## Licença

Este projeto não declara uma licença específica. Adicione uma licença ao repositório caso ele seja distribuído publicamente.
