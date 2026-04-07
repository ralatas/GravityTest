# GravityTest

Small Unity prototype where the player moves around a rectangular gravity path around a platform.

## Structure

- `Assets/Scripts/Bootstrap`
  - `GameOrchestrator` builds the runtime graph of services and views.
- `Assets/Scripts/Models`
  - `GameConfig` stores movement parameters and prefab references.
- `Assets/Scripts/Services`
  - `GameConfig` loads config from `Resources`
  - `GameViewFactory` instantiates `PlatformView`, `PlayerView`, `MobileControlsView`
  - `RectangleGravityPath` computes the path around the platform
  - `PlayerMotor` handles movement and jump logic
  - `Input` combines keyboard and mobile input
  - `ServiceLocator` is a simple static service registry
- `Assets/Scripts/Views`
  - runtime views for player, platform, and mobile controls

## Runtime flow

On startup `GameOrchestrator`:

1. Loads `GameConfig` from `Resources`
2. Registers services
3. Creates `PlatformView` and `PlayerView` under `worldRoot`
4. Creates `MobileControlsView` under `uiRoot`
5. Starts `PlayerPresenter`

`PlayerMotorService` computes the player position on the path.
`PlayerPresenter` applies that state to `PlayerView`.

## Unity setup

### 1. Create config

Required asset:

- `Assets/Resources/GameConfig.asset`

Assign these prefab references in `GameConfig`:

- `Platform View Prefab`
- `Player View Prefab`
- `Mobile Controls View Prefab`

Gameplay values in `GameConfig`:

- `Platform Size`
- `Platform Center`
- `Surface Offset`
- `Corner Radius`
- `Move Speed`
- `Jump Speed`
- `Gravity`
- `Player Size`

### 2. Prepare prefabs

#### PlatformView prefab

Must contain:

- `PlatformView`
- the platform visual you want to use

Platform position is applied from `GameConfig`.
Platform scale is taken from the prefab itself and is not overridden by code.

#### PlayerView prefab

Must contain:

- `PlayerView`
- the player visual

Player size is applied from `GameConfig`.

#### MobileControlsView prefab

Must contain:

- `MobileControlsView`
- three `Button` references:
  - left
  - right
  - jump

Assign all three buttons into `MobileControlsView`.

`left` and `right` use hold input.
`jump` uses `Button.onClick`.

### 3. Configure scene

Scene must contain an object with `GameOrchestrator`.

Assign:

- `worldRoot : Transform`
- `uiRoot : RectTransform`

Instantiation targets:

- `PlatformView` and `PlayerView` are created under `worldRoot`
- `MobileControlsView` is created under `uiRoot`

Scene must also contain:

- `Main Camera`
- `EventSystem`
- a Canvas with a `RectTransform` used as `uiRoot`

## Controls

### Keyboard

- `A` / `LeftArrow` - move left
- `D` / `RightArrow` - move right
- `Space` / `W` / `UpArrow` - jump

### Mobile UI

- hold `left` / `right` - move
- press `jump` - jump

## Notes

- `GameConfig` is loaded through `Resources.Load("GameConfig")`, so the asset must be located at `Assets/Resources/GameConfig.asset`
- prefab references are stored directly inside `GameConfig`
- if a prefab reference is missing in `GameConfig`, startup will fail with `MissingReferenceException`
