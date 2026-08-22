# Validation notes

## Existing test coverage

`Services.Tests` is an NUnit test project. It contains focused tests for:

- Interval and relative-point readers.
- File reading.
- Unit conversion.
- Two-dimensional mesh positions and triangle indices.
- Three-dimensional mesh validity, positions, triangle indices, and enclosed volume.

The most illustrative tests are in `Services.Tests/ThreeDimensionsMeshServiceTests.cs`. They construct simple rectangular volumes and verify that the mesh produces the expected vertices and a volume of 1, 2, or 8 units.

## Baseline verification process

Because the project targets .NET Framework 4.5, validation must run in a compatible Windows/Visual Studio environment rather than through a modern cross-platform `dotnet test` workflow.

1. Restore the legacy NuGet packages.
2. Build `OilUtils/OilUtils.sln`.
3. Run the `Services.Tests` NUnit suite from Visual Studio Test Explorer or a compatible NUnit runner.
4. Start the WPF host with the sample depth file to manually confirm module discovery and rendering.

## Current baseline result

On 2026-08-22, a Release build was attempted from this repository with the locally installed .NET SDK. It did not reach compilation because the workstation does not have the .NET Framework 4.5 Developer Pack/reference assemblies required by every project in the solution (`MSB3644`). No source change was made as a workaround.

The next verification step is to install a compatible .NET Framework targeting pack, restore packages if needed, then repeat the solution build and run the NUnit suite. Until that environment is available, this document describes the test intent and verification procedure rather than asserting a passing build.
