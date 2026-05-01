<div align="center">

# 📚 Clube da Leitura

### Sistema de gerenciamento de clube de leitura desenvolvido em **C# com POO**
Controle de revistas, caixas e empréstimos com separação de responsabilidades e regras de negócio reais.

---

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Console App](https://img.shields.io/badge/Console-Application-black?style=for-the-badge&logo=windows-terminal&logoColor=white)
![OOP](https://img.shields.io/badge/Paradigm-OOP-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Concluído-brightgreen?style=for-the-badge)

</div>

---

## 📌 Sobre o projeto

O **Clube da Leitura** é um sistema de console desenvolvido em **C# com Programação Orientada a Objetos**, com o objetivo de simular o gerenciamento completo de um clube de leitura.

O sistema permite organizar **revistas**, controlar **caixas** de armazenamento e registrar **empréstimos**, aplicando conceitos sólidos de POO como encapsulamento, separação de responsabilidades e construção de regras de negócio reais.

---

## ✅ Funcionalidades

- 📖 Cadastro e gerenciamento de revistas
- 📦 Controle de caixas de armazenamento
- 🔄 Registro e acompanhamento de empréstimos
- 🗂️ Menu interativo no console
- ✔️ Validação de operações inválidas
- 👁️ Visualização dos dados cadastrados

---

## 🧠 Conceitos aplicados

| Conceito | Aplicação |
|---|---|
| 🏗️ Classes e Objetos | Modelagem de Revista, Caixa e Empréstimo |
| 🔒 Encapsulamento | Proteção dos dados internos de cada entidade |
| 📐 Separação de Responsabilidades | Divisão clara entre UI, regras e ponto de entrada |
| ⚙️ Regras de negócio | Validações e lógica de controle de empréstimos |
| 🖥️ Interface via Console | Menu navegável e amigável ao usuário |
| 🔗 Comunicação entre classes | Objetos interagindo de forma coesa |

---

## 📂 Estrutura do projeto

```bash
📦 reading-club-adp
 ┗ 📁 ClubeDaLeitura.ConsoleApp
    ┣ 📜 Revista.cs           # Modelo da revista
    ┣ 📜 Caixa.cs             # Modelo da caixa de armazenamento
    ┣ 📜 Emprestimo.cs        # Modelo do empréstimo
    ┣ 📜 TelaPrincipal.cs     # Interface e navegação do console
    ┗ 📜 Program.cs           # Ponto de entrada da aplicação
```

---

## 📄 Responsabilidade dos arquivos

### `Revista.cs`
Responsável pela **modelagem de dados da revista**.
- Número da edição
- Título / Assunto
- Data de publicação
- Validações do modelo

### `Caixa.cs`
Responsável pelo **controle das caixas de armazenamento**.
- Identificador da caixa
- Revistas contidas
- Mês e ano de referência
- Gerenciamento de capacidade

### `Emprestimo.cs`
Responsável pelo **registro e controle de empréstimos**.
- Dados do solicitante
- Caixa emprestada
- Data de entrega
- Controle de devolução

### `TelaPrincipal.cs`
Responsável pela **interação com o usuário no console**.
- Menu principal e sub-menus
- Entrada e validação de dados
- Exibição das informações

### `Program.cs`
Responsável pelo **ponto de entrada da aplicação**.
- Método `Main()`
- Inicialização do sistema

---

## ⚙️ Tecnologias utilizadas

- **C#** — Linguagem principal
- **.NET** — Plataforma de execução
- **Console Application** — Interface de interação
- **POO** — Paradigma de desenvolvimento

---

## ▶️ Como executar

**1. Clone o repositório**
```bash
git clone https://github.com/pedrohenriquedsdev/reading-club-adp.git
```

**2. Acesse a pasta**
```bash
cd reading-club-adp/ClubeDaLeitura.ConsoleApp
```

**3. Execute o projeto**
```bash
dotnet run
```

> Requisito: [.NET SDK](https://dotnet.microsoft.com/download) instalado na máquina.

---

## 🎯 Objetivo de aprendizado

Este projeto foi desenvolvido para consolidar:

- ✔️ Pensamento orientado a objetos
- ✔️ Modelagem de entidades do mundo real
- ✔️ Separação de responsabilidades entre camadas
- ✔️ Comunicação coesa entre classes
- ✔️ Simulação de regras de negócio reais
- ✔️ Organização limpa e legível do código

---

## 👨‍💻 Autor

<div align="center">

Desenvolvido por **Pedro Henrique** como parte dos estudos em **C# e Programação Orientada a Objetos**.

[![GitHub](https://img.shields.io/badge/GitHub-pedrohenriquedsdev-181717?style=for-the-badge&logo=github)](https://github.com/pedrohenriquedsdev)

</div>
