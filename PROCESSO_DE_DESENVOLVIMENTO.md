Para esse desafio de scraping, meu primeiro pensamento foi fazer da forma como já tinha feito antes em outro projeto, que é criar meu próprio "scraping" com regex e HttpClient do c#. Mas com as análises feitas, vi que ocorria o mesmo 
erro de decoding que fiquei um tempo tentando resolver, e para não perder tempo, decidi buscar uma  biblioteca que já cuida de tudo, desde a detecção de padrões dentro do html até o decoding e achei a Html Agility Pack.

Desse modo, com a biblioteca que facilita o trabalho e com o acesso a url principal, é possivel pegar cada link dos produtos e criar uma lista de urls a serem visitadas para pegar as informações de seus respectivos produtos. 
Com toda a estrutura de scraping dividida, a única informação que faltou foi a image_url, segui os links da documentação que foi disponibilizado e vi que as imagens seguem um padrão, pensei em somente concatenar strings que é apresentado
de duas formas na documentação, só que existem vários tipos de imagens com códigos "rev" diferente uma das outras, informação a qual é disponibilizada na api que eles fornecem, outrossim tem a forma mais básica que é concatenar o index da imagem 
seguida da resolução. Não achei viável nenhuma das alternativas, pois na primeira iria ter que consumir uma API e na segunda as imagens podem vim de forma aleatória ou não existir. Então
optei por pegar a própria imagem que é disponibilizada quando o produto é acessado (Caso tenha).

Vindo os 100 produtos, chegou a hora de automatizar a execução do código de scraping, fiquei procurando uma forma de automatizar a execução do código uma vez ao dia, encontrei diversas bibliotecas e uma delas foi a FluentSheduler, 
configurei com o projeto e funcionou, só que tava com acoplamento forte, então decidi refatorar o código e depender de interfaces para fazer o DI, mas após a refatoração, parou de funcionar, por conta que essa biblioteca não tem um
sistema para a Injenção de Dependência que trabalhe de forma nativa com o ASP.NET Core, ademais eu precisava de uma forma que o job seguisse o tempo de vida dos componentes da aplicação para não ocorrer problemas indesejados. 
Com todos esses problemas, decidi procurar outra biblioteca e encontrei o Quartz para ASP.NET Core, nela foi muito fácil configurar os jobs com dependências.

Dessa maneira, com a automatização da execução do scraping, chegou a hora de conectar com banco de dados e decidi usar MySql, pois já tenho experiência com sua configuração no asp.net. Tive problemas com o "code" do produto, 
pois são muito grandes para inteiros, então decidi salvar como varchar(255) e adicionar um index para otimizar o trabalho de busca pela API. Com o passar do tempo, tive outro problema,
em que o Quartz usava o mesmo DbContext que a API em si, então tive que criar mais um contexto e dividir um repositório geral, para coisas mais especifícas. Dessa forma, a parte de consulta deixei para o DataContext da API, e a 
parte de registrar e atualizar deixei para o DataContextQuartz, os separandos por interfaces e repositórios segregados. 
