# MateShop

`MateShop` é um jogo educativo 2D top-down feito na Unity.

No jogo, o jogador trabalha em uma loja, memoriza o pedido dos NPCs, resolve uma operação matemática para descobrir a quantidade, coleta o item correto e retorna ao balcão para fazer a entrega.

Este projeto foi desenvolvido como um projeto acadêmico para o curso de `TADS` (`Tecnologia em Análise e Desenvolvimento de Sistemas`).

## Ideia Principal
- Desafio de memória: lembrar o item pedido e a operação
- Desafio de matemática: calcular a quantidade correta
- Desafio de tempo e pontuação: alcançar a meta de entregas antes de atingir o limite de erros

## Loop Principal de Gameplay
1. Um NPC chega ao balcão
2. Um balão global de pedido mostra o item e a operação
3. O jogador memoriza o pedido
4. O jogador vai até a área dos itens
5. O jogador coleta o item correto na quantidade correta
6. O jogador retorna ao balcão e entrega
7. O jogo valida acerto ou erro e atualiza a pontuação

## Regras de Dificuldade
- `Facil`
  - operações apenas de adição e subtração
  - meta de vitória: `30` entregas corretas
- `Medio`
  - operações de adição, subtração, multiplicação e divisão
  - até 2 termos
  - meta de vitória: `40` entregas corretas
- `Dificil`
  - operações de adição, subtração, multiplicação e divisão
  - pode usar até 3 termos
  - operações com terceiro número usam apenas `+` e `-`
  - meta de vitória: `50` entregas corretas

Regra comum para todas as dificuldades:
- máximo de erros: `5`

## Principais Recursos
- menu inicial com seleção de dificuldade
- sistema de fila e spawn de NPCs
- balão de pedido
- balão de coleta do jogador
- painel de pontuação
- painel de pausa
- painel de tutorial
- painel de fim de jogo
- melhor tempo salvo localmente por dificuldade
- coleta e remoção rápidas ao segurar as teclas de interação

## Controles
- ação de movimento / `WASD`: mover
- `Shift`: correr
- `E`: coletar 1 item
- segurar `E`: ativar coleta contínua após um pequeno tempo
- `Q`: remover 1 item coletado quando estiver perto do item correspondente
- segurar `Q`: ativar remoção contínua após um pequeno tempo
- `ESC`: pausar

## Estrutura do Projeto
- `Assets/Scripts/Core`
  - configurações e enums compartilhados
- `Assets/Scripts/Gameplay`
  - fluxo principal, pontuação e lógica do balcão
- `Assets/Scripts/Items`
  - dados dos itens e interação dos itens
- `Assets/Scripts/Math`
  - listas de operações por dificuldade
- `Assets/Scripts/Menu`
  - fluxo do menu inicial
- `Assets/Scripts/NPC`
  - movimentação e spawn dos NPCs
- `Assets/Scripts/Player`
  - movimentação e interação do jogador
- `Assets/Scripts/UI`
  - HUD, balões, modais e feedbacks

## Cenas Principais
- `Assets/Scenes/Menu.unity`
- `Assets/Scenes/GamePlay.unity`

## Como Rodar na Unity
1. Abra o projeto na Unity
2. Abra a cena `Assets/Scenes/Menu.unity`
3. Pressione Play

## Documentação
Documentação interna detalhada do projeto:
- `PROJECT_GUIDE.md`

## Status
Este repositório representa atualmente a primeira versão jogável do jogo.
