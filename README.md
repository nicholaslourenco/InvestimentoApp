# InvestimentoApp - API de Cálculo de Rentabilidade

Esta é uma Web API desenvolvida em **.NET 10** para o cálculo de rentabilidade de investimentos baseada em taxas diárias de indexadores (como o CDI). O projeto foi construído seguindo boas práticas de arquitetura e garantindo precisão matemática financeira rigorosa.

## 🚀 Tecnologias Utilizadas

* **Runtime:** .NET 10
* **Linguagem:** C#
* **Banco de Dados:** SQLite (para portabilidade e facilidade de avaliação)
* **Documentação:** Swagger (Swashbuckle)
* **Testes:** xUnit e Moq
* **Logging:** Serilog/Microsoft Logging

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, dividido em:
* **Domain:** Entidades e interfaces de repositório.
* **Application:** Serviços de negócio (onde reside a lógica de cálculo).
* **Infrastructure:** Contexto do banco de dados (EF Core) e implementação de repositórios.
* **API:** Controllers e configurações de pipeline.

## 🧮 Lógica de Cálculo (Precisão Bancária)

Para atender aos requisitos do desafio, a lógica de cálculo foi implementada seguindo estas etapas críticas:

1.  **Seleção de Dados:** Período iniciado em `D+1` (inclusive) até a `Data Final` (exclusive).
2.  **Fator Diário:** Calculado pela fórmula $(1 + \frac{taxa}{100})^{1/252}$, com cada fator diário **arredondado na 8ª casa decimal** (`MidpointRounding.AwayFromZero`).
3.  **Fator Acumulado:** Resultado do produtório dos fatores diários, **truncado na 16ª casa decimal**.
4.  **Valor Atualizado:** Multiplicação do Valor Original pelo Fator Acumulado, **truncado na 8ª casa decimal**.

## 🛠️ Como Executar o Projeto

### Pré-requisitos
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.
* Visual Studio 2026 (ou VS Code com C# Dev Kit).

### Passo a Passo

1.  **Clonar o Repositório:**
    ```bash
    git clone [url-do-seu-repositorio]
    ```

2.  **Restaurar Pacotes:**
    ```bash
    dotnet restore
    ```

3.  **Ajuste do Banco de Dados (Importante):**
    O arquivo do banco de dados `Investimentos.db` está localizado na pasta `Banco de Dados`. No arquivo `appsettings.json`, verifique a `ConnectionString`. 
    * **Dica:** Por padrão, o caminho está configurado de forma relativa, mas caso ocorra erro de "Unable to open database file", você pode ajustar para o caminho absoluto no `appsettings.json` da máquina local.

4.  **Executar a API:**
    ```bash
    dotnet run --project InvestimentoApp.API
    ```

5.  **Acessar a Documentação:**
    Após iniciar, a interface do Swagger estará disponível em: `https://localhost:7121/swagger` (ou na porta configurada no seu `launchSettings.json`).

## 🧪 Executando Testes Unitários

O projeto conta com uma suíte de testes unitários que valida o cenário oficial do PDF (Valor de R$ 10.000,00 entre 13/03/2025 e 21/03/2025).

Para rodar os testes via linha de comando:
```bash
dotnet test
