# OilUtils

OilUtils is a WPF application for modeling layered subsurface volumes from depth-point data. It reads surface points, constructs two-dimensional and three-dimensional meshes, renders them through a Prism-based desktop interface, and calculates enclosed volume.

![Existing class diagram](Class%20Diagram.PNG)

## Capabilities and structure

- A modular WPF application composed with Prism and Unity.
- Explicit service and operation contracts for reading points, building meshes, and converting units.
- Separate two-dimensional and three-dimensional mesh services.
- Calculation of triangulated mesh indices and enclosed volume from upper and lower surfaces.
- NUnit coverage for reader, unit-conversion, mesh, and volume-calculation behavior.

The most interesting implementation is `Services/ThreeDimensionsMeshService.cs`: it converts paired upper/lower depth surfaces into mesh positions and triangle indices, then calculates the enclosed volume. The tests include cube-shaped fixtures that make the mesh and volume logic easy to reason about.

## Repository guide

- [Architecture](docs/architecture.md) explains the modules, contracts, and data flow.
- [Validation notes](docs/validation.md) identify the existing automated tests and the baseline-verification process.
- [Platform and maintenance notes](docs/legacy.md) records the runtime, build constraints, and design trade-offs that should be understood before using the code.
- [`readme`](readme) and [`Services Documentation`](Services%20Documentation) are the original run notes and contract descriptions retained for historical context.

## Running the application

OilUtils targets .NET Framework 4.5 and was authored for Visual Studio 2019-era tooling. A compatible Windows environment requires:

1. Visual Studio with .NET desktop development support and a compatible .NET Framework targeting pack.
2. NuGet package restore.
3. Opening `OilUtils/OilUtils.sln` and building the solution.
4. Ensuring `Services.dll` and `LayeringControlLibrary.dll` are available beside the WPF executable, as required by the Prism module configuration.

The sample input file is `OilUtils/Content/TopHorizonDepths.txt`. The original run notes describe the remaining manual setup details.

## Scope and limitations

The project targets .NET Framework 4.5 and depends on WPF, Prism, Unity, and the legacy NuGet `packages.config` layout. It has no current CI pipeline or cross-platform runtime path. Mesh-service instances retain calculated positions, triangle indices, and volume until `Reset()` is called, so callers must manage each service instance and input lifecycle accordingly.
