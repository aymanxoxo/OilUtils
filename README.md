# OilUtils

OilUtils is a legacy WPF application that models layered subsurface volumes from depth-point data. It is preserved as an engineering case study: the project demonstrates modular desktop composition, geometry calculation, and automated verification, while remaining candid about its age and limitations.

> This repository intentionally leaves the application code unchanged. The documentation added around it explains the existing design; it does not present OilUtils as a current production-ready system.

![Existing class diagram](Class%20Diagram.PNG)

## What the project demonstrates

- A modular WPF application composed with Prism and Unity.
- Explicit service and operation contracts for reading points, building meshes, and converting units.
- Separate two-dimensional and three-dimensional mesh services.
- Calculation of triangulated mesh indices and enclosed volume from upper and lower surfaces.
- NUnit coverage for reader, unit-conversion, mesh, and volume-calculation behavior.

The most interesting implementation is `Services/ThreeDimensionsMeshService.cs`: it converts paired upper/lower depth surfaces into mesh positions and triangle indices, then calculates the enclosed volume. The tests include cube-shaped fixtures that make the mesh and volume logic easy to reason about.

## Repository guide

- [Architecture](docs/architecture.md) explains the modules, contracts, and data flow.
- [Validation notes](docs/validation.md) identify the existing automated tests and the baseline-verification process.
- [Legacy notes](docs/legacy.md) records the runtime, build constraints, and design trade-offs that should be understood before using the code.
- [`readme`](readme) and [`Services Documentation`](Services%20Documentation) are the original run notes and contract descriptions retained for historical context.

## Running the legacy application

OilUtils targets .NET Framework 4.5 and was authored for Visual Studio 2019-era tooling. A compatible Windows environment requires:

1. Visual Studio with .NET desktop development support and a compatible .NET Framework targeting pack.
2. NuGet package restore.
3. Opening `OilUtils/OilUtils.sln` and building the solution.
4. Ensuring `Services.dll` and `LayeringControlLibrary.dll` are available beside the WPF executable, as required by the Prism module configuration.

The sample input file is `OilUtils/Content/TopHorizonDepths.txt`. The original run notes describe the remaining manual setup details.

## Scope and limitations

This is not a modern .NET service or an actively maintained product. It uses .NET Framework, WPF, Prism, Unity, and `packages.config`; it has no current CI pipeline or cross-platform support. The service implementations are stateful and some boundaries would be redesigned today for safer validation, clearer ownership, and more efficient data handling.

Those constraints are part of the case study, not details to conceal. The repository is useful because the domain problem, modularization, and tested geometry behavior remain visible and explainable.
