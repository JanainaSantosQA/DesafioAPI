<h1>Automação: Mantis Bug Tracker REST API</h1>

<a href="https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP">Documentação oficial da API</a>

Projeto desenvolvido em C# - VS2019.


<b>Metas do projeto:</b>

1) Implementar 50 scripts de testes que manipulem uma aplicação cuja interface é uma API REST. 
Foram criados 53 scripts de testes distribuídos nas classes: DeleteAFilterTests, GetAFilterTests, AddAnIssueRelatiobshipTests, AttachATagToIssueTests, CreateAnIssueTests,
DeleteAnIssueNoteTests, DeleteAnIssueTests, AddSubProjectTests, CreateProjectTests, CreateProjectVersionTests, DeleteProjectTests, GetProjectTests, UpdateProjectTests,
UpdateSubProjectTests, CreateUserTests, DeleteUserTests e GetMyUserInfoTests.
2) Alguns scripts devem ler dados de uma planilha Excel para implementar Data-Driven. Item implementado na classe: CreateUserTests
3) Notem que 50 scripts podem cobrir mais de 50 casos de testes se usarmos Data-Driven. Em outras palavras, implementar 50 CTs usando data-driven não é a 
mesma coisa que implementar 50 scripts.
4) O projeto deve tratar autenticação. Exemplo: OAuth2. As chamadas da API REST do Mantis devem ser autenticadas. 
Para criar o Token acessei a aplicação do Mantis com o usuário criado depois fui no: "Menu com nome do usuário logado > Minha Conta > Tokens API > Informe o "Nome do Token" > Clique no botão "Criar Token API" "
Depois do token gerado ele foi utizado no header "Authorization".
5) Pelo menos um teste deve fazer a validação usando REGEX (Expressões Regulares): implementado nas classes: AddAnIssueRelatiobshipTests, AttachATagToIssueTests,
CreateAnIssueTests, DeleteAnIssueNoteTests, DeleteAnIssueTests e DeleteUserTests.
6) Pelo menos um script deve usar código Groovy / Node.js ou outra linguagem para fazer scripts.
Exemplos: para reutilizar um passo de outro teste, para calcular o CPF, iterar em uma lista de valores retornados em uma chamada, fazer asserts.
Foi implementado um código em JS na classe DeleteAnIssueNoteTests para validar o status code retornado.
7) O projeto deverá gerar um relatório de testes automaticamente: foi o utilizado o framework ExtentReports.
8) Implementar pelo menos dois ambientes (desenvolvimento / homologação). Item implementado na classe: GeneralHelpers.
9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados. Se usar <a href="http://wiremock.org/">WireMock</a> a massa será tratada implicitamente. 
10) Executar testes em paralelo. Pelo menos duas threads (25 testes cada): foi utilizado o atributo assembly-level para especificar o nível de paralelismo. 
Implementado na classe: "AutomacaoApiMantis.AssemblyInfo.cs".
11) Testes deverão ser agendados pelo Jenkins, CircleCI, TFS ou outra ferramenta de CI que preferir: foi utilizado o Azure DevOps.
O arquivo com a configuração realizada se encontra no repositório, caminho: AutomacaoApiMantis\AutomacaoApiMantis\Resources\Configuracao_Azure_Devops.pdf

<a href="https://dev.azure.com/janainasantos033/DesafioAPI/_build/results?buildId=28&view=results">Execução no Azure DevOps<a/>

