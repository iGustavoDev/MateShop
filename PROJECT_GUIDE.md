# MateShop Project Guide

## Overview
`MateShop` is a 2D top-down educational Unity game.

Core loop:
1. An NPC arrives at the counter.
2. The NPC shows an item image and a math operation in a global request bubble.
3. The player memorizes the request.
4. The player walks to the item shelves, collects the requested item, and returns to the counter.
5. The game checks item type + quantity and shows centered feedback.

Design intent:
- Encourage memory, attention, and basic arithmetic.
- Keep HUD minimal.
- Use in-world bubbles instead of always-visible UI instructions.

## Current Scene Structure
Main gameplay scene:
- `Assets/Scenes/GamePlay.unity`

Menu scene:
- `Assets/Scenes/Menu.unity`

Scripts folder structure:
- `Assets/Scripts/Core`
  - shared game state and enums
- `Assets/Scripts/Gameplay`
  - gameplay flow and validation
- `Assets/Scripts/Items`
  - item data and world item behavior
- `Assets/Scripts/Math`
  - operation definitions and difficulty-specific math lists
- `Assets/Scripts/Menu`
  - menu scene flow
- `Assets/Scripts/NPC`
  - NPC movement and spawn systems
- `Assets/Scripts/Player`
  - player movement and interaction
- `Assets/Scripts/UI`
  - HUD and bubble UI logic

Current hierarchy:
- `GamePlay`
  - `Main Camera`
  - `Global Light 2D`
  - `Player`
    - `Visual`
      - `Hair`
      - `Hand`
    - `BalaoColeta`
      - `Canvas`
        - `Balao`
        - `IconeItem`
        - `TextoQuantidade`
  - `Grid`
    - `TileBackGround`
    - `TileBackGroundGrass`
    - `TileFontes`
      - `TilePontesChao`
      - `TilePontesParedes`
      - `TilePontesColunas`
    - `TileEstradas`
    - `TileMesas`
    - `TileCasas`
    - `TileArvores`
    - `TileFlorestas`
    - `TileArbustos`
    - `TileTroncos`
    - `TileFlores`
    - `TilePedras`
    - `TileBalcao`
  - `CM Principal`
  - `Items`
  - `GameManager`
  - `EventSystem`
  - `UIManager`
  - `Canvas`
    - `FeedbackRapido`
      - `Background`
      - `Conteudo`
        - `IconeFeedback`
        - `TextoFeedback`
    - `PainelPontuacao`
      - `Background`
      - `Conteudo`
        - `ConteudoAcertos`
          - `IconeAcertos`
          - `TextoAcertos`
        - `ConteudoErros`
          - `IconeErros`
          - `TextoErros`
    - `PainelItemColetado`
      - `Background`
      - `Conteudo`
        - `IconeItem`
        - `TextoQuantidade`
    - `LayerModais`
      - `PainelAviso`
        - `Background`
        - `Conteudo`
          - `TituloAviso`
          - `TextoAviso`
          - `BotaoConfirmar`
      - `PainelPausa`
        - `Background`
        - `Conteudo`
          - `TituloPausa`
          - `TextoPausa`
          - `BotaoContinuar`
          - `BotaoReiniciar`
          - `BotaoMenu`
      - `PainelFimDeJogo`
        - `Background`
        - `Conteudo`
          - `TituloFimDeJogo`
          - `TextoFimDeJogo`
          - `BotaoPrimario`
          - `BotaoSecundario`
      - `PainelTutorial`
        - `Background`
        - `Conteudo`
          - `TituloTutorial`
          - `TextoTutorial`
          - `BotaoComecar`
  - `Balcao`
  - `NPC` `(prefab)`
    - `Hair`
    - `Hand`
  - `PontosNoMapa`
    - `PontoSaida`
    - `PontoSpawn`
  - `Fila`
    - `PontoFila0`
    - `PontoFila1`
    - `PontoFila2`
    - `PontoFila3`
  - `FilaManager`
  - `NPCSpawner`
  - `BalaoPedido`
    - `Canvas`
      - `Balao`
      - `IconeItem`
      - `TextoOperacao`
  - `AudioGameplay`
Menu hierarchy:
- `Menu`
  - `Main Camera`
  - `Canvas`
    - `Background`
    - `Logo`
    - `MenuPrincipal`
      - `BotaoJogar`
      - `BotaoSair`
    - `MenuDificuldade`
      - `BotaoFacil`
      - `BotaoMedio`
      - `BotaoDificil`
  - `EventSystem`
  - `MenuManager`

## Main Systems

### Menu Flow
Scripts:
- `Assets/Scripts/Menu/MenuManager.cs`
- `Assets/Scripts/Core/GameSettings.cs`
- `Assets/Scripts/Core/GameDifficulty.cs`

Responsibilities:
- Control the initial menu flow.
- Hide the main buttons after clicking `Jogar`.
- Show the difficulty selection buttons.
- Store the selected difficulty before loading `GamePlay`.
- Load the gameplay scene.

Expected button wiring:
- `BotaoJogar` -> `MenuManager.AbrirSelecaoDeDificuldade()`
- `BotaoSair` -> `MenuManager.Sair()`
- `BotaoFacil` -> `MenuManager.JogarFacil()`
- `BotaoMedio` -> `MenuManager.JogarMedio()`
- `BotaoDificil` -> `MenuManager.JogarDificil()`

Current note:
- Difficulty selection is already stored globally in `GameSettings`.
- Gameplay difficulty rules can now be implemented later by reading `GameSettings.SelectedDifficulty`.

### Player
Script:
- `Assets/Scripts/Player/PlayerController.cs`

Responsibilities:
- Reads movement input with the Unity Input System.
- Moves the player with `Rigidbody2D`.
- Supports running while `Shift` is pressed.
- Updates the animation parameter `Movimento` with:
  - `0` for idle
  - `1` for walking
  - `2` for running
- Flips only the visual root, not the whole player root.
- Tracks nearby item triggers.
- Keeps focus on the first item entered until that item is fully exited.
- Handles collect with `E`.
- Handles remove with `Q`.
- Updates the player collection bubble.

Important Inspector references:
- `balaoColetaUI`
- `visualRoot`

Player hierarchy:
- `Player`
  - logic, collider, rigidbody, controller script
  - `Visual`
    - animator and visual children such as `Hair` and `Hand`
  - `BalaoColeta`
    - separate from `Visual` so it does not flip

### Item Interaction
Script:
- `Assets/Scripts/Items/Item.cs`

Responsibilities:
- Holds an `ItemData` reference.
- Updates its world sprite from `ItemData`.
- Notifies the player when entering/exiting the trigger.
- Refreshes the editor preview sprite safely when `ItemData` changes in the Inspector.

Important rule:
- Items do not directly process input anymore.
- Player interaction is centralized in `PlayerController`.

### Item Data
Script:
- `Assets/Scripts/Items/ItemData.cs`

Type:
- `ScriptableObject`

Responsibilities:
- Defines item identity and visuals.

Current fields:
- `id`
- `nomeExibicao`
- `sprite`

Usage:
- Each collectible world item prefab should reference one `ItemData`.
- `GameManager` request generation uses a catalog of `ItemData`.

### Request System
Scripts:
- `Assets/Scripts/Gameplay/GameManager.cs`
- `Assets/Scripts/UI/BalaoPedidoUI.cs`
- `Assets/Scripts/Math/OperacoesPorDificuldade.cs`
- `Assets/Scripts/Math/OperacoesFacil.cs`
- `Assets/Scripts/Math/OperacoesMedio.cs`
- `Assets/Scripts/Math/OperacoesDificil.cs`

Responsibilities:
- Stores current requested item.
- Stores current math operation.
- Generates random requests from:
  - item catalog
  - operation list for the selected difficulty
- Shows/hides the global NPC request bubble.

Notes:
- Request bubble is global, not attached to the NPC.
- Request is hidden again after delivery is resolved.
- Operation text is formatted for children:
  - multiplication displays as `x`
  - division displays as `÷`
- Operation lists are separated by difficulty in dedicated code files.
- `GameManager` no longer owns the curated operation definitions directly.
- The request bubble now safely hides its icon if no sprite is provided.

### Collection System
Scripts:
- `Assets/Scripts/Gameplay/GameManager.cs`
- `Assets/Scripts/UI/BalaoColetaUI.cs`

Responsibilities:
- Stores current collected item.
- Stores current collected quantity.
- Limits max quantity to `99`.
- Shows the quantity in the player collection bubble.
- Shows `0` when the player is near an item and has not collected any of that item yet.
- Notifies listeners immediately when collection data changes.

Behavior:
- Player can only hold one active item type at a time.
- If a different item is collected, the previous stack is discarded.
- The new item starts again from `1`.
- The collection bubble refreshes immediately after collect, remove, or delivery reset.

### NPC and Queue
Scripts:
- `Assets/Scripts/NPC/NPCCliente.cs`
- `Assets/Scripts/NPC/NPCSpawner.cs`
- `Assets/Scripts/Gameplay/FilaManager.cs`

Responsibilities:
- Spawn NPCs over time.
- Place NPCs in queue positions.
- Move the first NPC to the counter.
- Generate a request when the first NPC reaches the counter.
- Send NPC away after delivery.

Important Inspector references:
- `NPCSpawner` needs:
  - NPC prefab
  - spawn point
  - exit point
- `FilaManager` needs ordered queue points

### Counter Validation
Script:
- `Assets/Scripts/Gameplay/Balcao.cs`

Responsibilities:
- Detect when player is in the counter area.
- Warn the player if delivery is attempted without any collected item.
- Validate:
  - requested item vs collected item
  - requested quantity vs collected quantity
- Register successful deliveries and errors in `GameManager`
- Show centered feedback through `UIManager`.
- Reset collection state.
- Clear current request.

Current behavior:
- NPC leaves after delivery attempt.
- If this should only happen on correct delivery, that logic can be changed later.

### Minimal HUD
Script:
- `Assets/Scripts/UI/UIManager.cs`

Responsibilities:
- Shows centered feedback text with icon and sound.
- Shows score summary in the top-right corner.
- Shows the currently collected item and quantity in a separate panel.
- Controls the warning modal panel.
- Controls the pause panel.
- Controls the tutorial panel shown at the start of gameplay.
- Updates:
  - successful deliveries
  - current errors out of the max limit

Current design:
- No always-visible HUD for request or quantity.
- Memory challenge is intentional.
- Score panel remains visible.
- Collected item panel is visible only while the player is carrying something.
- Delivery feedback is shown as a short centered popup with text + icon.
- Pause is opened with `ESC`.
- Gameplay starts with a blocking tutorial panel.

### Score System
Scripts:
- `Assets/Scripts/Gameplay/GameManager.cs`
- `Assets/Scripts/UI/UIManager.cs`
- `Assets/Scripts/Gameplay/Balcao.cs`

Responsibilities:
- Track successful deliveries.
- Track the delivery goal.
- Track current errors.
- Cap errors at `5`.
- Track whether the run has already ended.
- Track total gameplay time for the current run.
- Persist the fastest successful completion time on the local machine.
- Expose the score state to UI.

Current display:
- Successes: `current/meta`
- Errors: `current/5`

Current rule:
- Correct delivery increments successes.
- Wrong delivery increments errors.
- Delivery goal now depends on difficulty:
  - `Facil`: `30`
  - `Medio`: `40`
  - `Dificil`: `50`
- Reaching the current goal triggers victory.
- Reaching `5/5` errors triggers defeat.
- After victory or defeat, gameplay is considered ended.

### Difficulty-based Operations
Scripts:
- `Assets/Scripts/Math/OperacoesPorDificuldade.cs`
- `Assets/Scripts/Math/OperacoesFacil.cs`
- `Assets/Scripts/Math/OperacoesMedio.cs`
- `Assets/Scripts/Math/OperacoesDificil.cs`

Responsibilities:
- Keep the math content separated from scene flow.
- Allow bulk editing of operations directly in code.
- Return the correct curated list for the selected difficulty.
- Keep inline comments with expected results beside curated operations for easier maintenance.

Current rule:
- `Facil`: only addition and subtraction.
- `Medio`: addition, subtraction, multiplication, and division, with up to 2 terms.
- `Dificil`: the 4 operations, with operations that can use up to 3 terms.
- Each difficulty currently has 30 curated operations in code.
- All curated operations must keep results:
  - non-negative
  - integer
  - at most `99`
- In `Dificil`, any operation that uses a third number must use only `+` and `-`.

Implementation note:
- `GameManager` reads `GameSettings.SelectedDifficulty`
- then loads operations through `OperacoesPorDificuldade.Obter(...)`

## UI Systems

### Global Request Bubble
Script:
- `Assets/Scripts/UI/BalaoPedidoUI.cs`

Purpose:
- Show requested item icon + operation when NPC reaches the counter.

Expected references:
- `container`
- `iconeItem`
- `textoOperacao`

### Player Collection Bubble
Script:
- `Assets/Scripts/UI/BalaoColetaUI.cs`

Purpose:
- Show the nearby item icon over the player.
- Show current collected quantity for that focused item.

Expected references:
- `container`
- `iconeItem`
- `textoQuantidade`

Recommended setup:
- Use `World Space` canvas.
- Keep this bubble outside the visual object that flips.
- The bubble uses the same sprite configured in `ItemData`.

### Score Panel
Hierarchy:
- `Canvas`
  - `PainelPontuacao`
    - `Background`
    - `Conteudo`
      - `ConteudoAcertos`
        - `IconeAcertos`
        - `TextoAcertos`
      - `ConteudoErros`
        - `IconeErros`
        - `TextoErros`

Purpose:
- Display the running score without exposing the current request.

Suggested behavior:
- `TextoAcertos` shows correct deliveries as `0/meta`, `1/meta`, etc.
- `TextoErros` shows errors as `0/5`, `1/5`, etc.
- `IconeAcertos` uses the `check` asset.
- `IconeErros` uses the `X` asset.

### Collected Item Panel
Hierarchy:
- `Canvas`
  - `PainelItemColetado`
    - `Background`
    - `Conteudo`
      - `IconeItem`
      - `TextoQuantidade`

Purpose:
- Show which item the player is currently carrying and how many units are in hand.

Current behavior:
- Hidden when the player is not carrying any item.
- Shows the collected item sprite and current quantity when carrying something.
- Refreshes immediately after collect, remove, item swap, or delivery reset.

### Quick Feedback
Hierarchy:
- `Canvas`
  - `FeedbackRapido`
    - `Background`
    - `Conteudo`
      - `IconeFeedback`
      - `TextoFeedback`

Purpose:
- Show short non-blocking delivery feedback in the center of the screen.

Current behavior:
- Correct delivery:
  - `IconeFeedback` uses the success sprite
  - message `Certo!`
  - plays the configured success sound effect
- Wrong delivery:
  - `IconeFeedback` uses the error sprite
  - message `Errado!`
  - plays the configured error sound effect

### Warning Panel
Hierarchy:
- `Canvas`
  - `LayerModais`
    - `PainelAviso`
      - `Background`
      - `Conteudo`
        - `TituloAviso`
        - `TextoAviso`
        - `BotaoConfirmar`

Purpose:
- Show short blocking warnings using the same visual language planned for future modals.

Current use:
- Warn the player when trying to deliver at the counter without carrying any item.
- This panel currently uses text only and does not require an icon.

### Pause Panel
Hierarchy:
- `Canvas`
  - `LayerModais`
    - `PainelPausa`
      - `Background`
      - `Conteudo`
        - `TituloPausa`
        - `TextoPausa`
        - `BotaoContinuar`
        - `BotaoReiniciar`
        - `BotaoMenu`

Purpose:
- Pause gameplay and present navigation actions.

Current behavior:
- Opens with `ESC`.
- Sets `Time.timeScale = 0`.
- Allows the player to continue, restart the scene, or return to the menu.

### Tutorial Panel
Hierarchy:
- `Canvas`
  - `LayerModais`
    - `PainelTutorial`
      - `Background`
      - `Conteudo`
        - `TituloTutorial`
        - `TextoTutorial`
        - `BotaoComecar`

Purpose:
- Explain the game objective and controls when entering `GamePlay`.

Current behavior:
- Opens automatically when the gameplay scene starts.
- Pauses the game with `Time.timeScale = 0`.
- Closes when the player presses the `Começar` button.
- While the tutorial is open, pause with `ESC` is blocked.

### End Game Panel
Hierarchy:
- `Canvas`
  - `LayerModais`
    - `PainelFimDeJogo`
      - `Background`
      - `Conteudo`
        - `TituloFimDeJogo`
        - `TextoFimDeJogo`
        - `BotaoPrimario`
        - `BotaoSecundario`

Purpose:
- Show the final result of the run after victory or defeat.

Current behavior:
- Victory:
  - title indicates the player won
  - text shows current successes, errors, total run time, and best local time
  - primary button text: `Jogar novamente`
  - secondary button text: `Voltar ao menu`
- Defeat:
  - title indicates the player lost
  - text shows current successes, errors, total run time, and best local time
  - primary button text: `Tentar novamente`
  - secondary button text: `Voltar ao menu`
- Opening this panel pauses the game with `Time.timeScale = 0`.

## Debug Testing
Current editor-only shortcuts inside Unity:
- `F8` -> adds `+1` error
- `F9` -> adds `+1` success
- `F10` -> adds `+10` successes

Notes:
- These shortcuts only compile in the Unity Editor.
- They are useful for testing score UI and victory/defeat panels without playing full rounds.

### Pause Panel
Hierarchy:
- `Canvas`
  - `LayerModais`
    - `PainelPausa`
      - `Background`
      - `Conteudo`
        - `TituloPausa`
        - `TextoPausa`
        - `BotaoContinuar`
        - `BotaoReiniciar`
        - `BotaoMenu`

Purpose:
- Pause gameplay and present navigation actions.

Current behavior:
- Opens when the player presses `ESC`.
- Sets `Time.timeScale = 0`.
- Allows:
  - continue the current game
  - restart the current gameplay scene
  - return to the `Menu` scene

## Input
Movement:
- Unity Input System via `InputSystem_Actions`

Interaction:
- `E` to collect focused item
- `Q` to remove one unit from current collected stack only when near the matching item
- `Shift` to run

Note:
- Movement uses the new Input System.
- Item interaction still uses `Input.GetKeyDown` for the action keys.

## Important Project Conventions
- Prefer `ItemData` over enums for item identity.
- Keep gameplay logic on root objects, visuals on dedicated visual children.
- Avoid flipping full object roots when they contain UI children.
- Keep NPC request display and player collection display as separate systems.
- Prefer Inspector references over `FindObjectOfType` or tag lookups when practical.

## Current Known Constraints
- The request bubble is global, not per-NPC.
- Delivery currently resolves the NPC immediately after an attempt.
- Collection is single-item-type at a time.
- End-of-run flow already exists through the victory/defeat panel.

## Good Next Steps
- Move operation/item request ownership into each NPC if per-customer logic becomes necessary.
- Add better success/failure consequences at the counter.
- Add sound and animation feedback for max stack, wrong delivery, and correct delivery.
- Improve UI polish for both bubbles.
- Implement gameplay difficulty effects beyond the curated operation lists.

## Files Most Relevant For AI Assistance
- `Assets/Scripts/Math/OperacoesPorDificuldade.cs`
- `Assets/Scripts/Math/OperacoesFacil.cs`
- `Assets/Scripts/Math/OperacoesMedio.cs`
- `Assets/Scripts/Math/OperacoesDificil.cs`
- `Assets/Scripts/Menu/MenuManager.cs`
- `Assets/Scripts/Core/GameSettings.cs`
- `Assets/Scripts/Core/GameDifficulty.cs`
- `Assets/Scripts/Player/PlayerController.cs`
- `Assets/Scripts/Items/Item.cs`
- `Assets/Scripts/Items/ItemData.cs`
- `Assets/Scripts/Gameplay/GameManager.cs`
- `Assets/Scripts/UI/BalaoPedidoUI.cs`
- `Assets/Scripts/UI/BalaoColetaUI.cs`
- `Assets/Scripts/NPC/NPCCliente.cs`
- `Assets/Scripts/NPC/NPCSpawner.cs`
- `Assets/Scripts/Gameplay/FilaManager.cs`
- `Assets/Scripts/Gameplay/Balcao.cs`
- `Assets/Scripts/UI/UIManager.cs`

## Summary
This project is a memory-and-math-driven shop game prototype.
The current architecture is already organized around:
- data-driven items with `ItemData`
- player-centered collection interaction
- queue-based NPC requests
- curated operation lists by difficulty
- minimal HUD and in-world visual prompts
- persistent score tracking for successes and mistakes

Any future changes should preserve those pillars unless intentionally redesigning the core game loop.


