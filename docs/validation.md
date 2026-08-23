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

## Verification status

This repository does not include a recorded CI result. The procedure above documents how to build and exercise the existing test suite; it does not claim a current passing build.
