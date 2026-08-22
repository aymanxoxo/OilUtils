# Legacy notes

OilUtils is intentionally documented, not rewritten.

## Runtime and tooling

- Target framework: .NET Framework 4.5.
- Application type: Windows-only WPF desktop application.
- Composition: Prism modules with Unity.
- Dependencies: legacy NuGet `packages.config` layout.
- Test framework: NUnit 3.

The solution records Visual Studio 2019 metadata, but a compatible .NET Framework targeting pack and package restore are still required to build it today.

## Operational constraints

- Prism discovers `Services.dll` and `LayeringControlLibrary.dll` through `OilUtils/App.config`; the module binaries must be present next to the host application.
- The sample depth input is a local file and can be changed through the `demoDepthsFile` setting.
- There is no CI workflow, container setup, web API, or cross-platform runtime path.

## What a modern redesign would revisit

- Use supported .NET and package-management tooling.
- Model vertices and meshes with typed structures rather than UI-oriented formatted strings.
- Make mesh calculations stateless or explicitly immutable per request.
- Strengthen null/shape validation at service boundaries.
- Add a reproducible build, automated test execution, and dependency scanning.

None of those changes are included in this documentation pass. Keeping the original code intact makes the distinction between the historical implementation and current engineering judgment clear.
