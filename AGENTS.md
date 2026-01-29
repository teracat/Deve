# Instructions for Agents

## Objective
- Maintain the project's current style and apply only the necessary changes.
- Respect the patterns and conventions that already exist in each folder/file.

## Style and Formatting
- Use CRLF, 4 spaces, and avoid reformatting code you haven't touched.
- Use UTF‑8 with BOM in files, and ensure that added or existing characters follow this encoding (UTF‑8 with BOM).
- Avoid inconsistent line endings (clean CRLF, without duplicated CR).
- Keep the order of `using` statements and the namespace layout as seen in the project.
- Prefer `var` when the type is clear, and use braces `{}` in blocks (following the current style).
- Place `using` statements outside the namespace, ordering `System.*` first (without separating groups).
- Prefer predefined types (`int`, `string`) instead of BCL types.
- Use parentheses for clarity in expressions with binary operators.
- Maintain the current C# formatting style (spaces around operators, new lines before `else/catch/finally`, etc.).
- Expression preferences: prefer `??`, object/collection initializers, null‑propagation (`?.`), auto‑properties, and simplified expressions when possible.
- Expression‑bodied members: prefer expression‑bodied form in accessors, properties, indexers and lambdas; in methods only if they are one‑liners; avoid them in constructors and operators.

## Naming
- Classes, methods, and properties in PascalCase.
- Private fields in `_camelCase`.
- Parameters and local variables in camelCase.
- Constants in PascalCase.
- DTOs and models should use a clear suffix (`Request`, `Response`) according to their existing usage.

## Code Structure
- C# and .NET: maintain the class, property, and method style used in the file you're working in.
- Avoid large renames or refactors unless justified.

## API and Endpoints
- Maintain consistency with existing routes (verbs, segments, and conventions).
- Responses must be consistent with existing endpoints (status code, shape, and field names).
- Avoid changing public contracts unless justified and agreed upon.
- Always document modified or new endpoints in detail. Add or update the corresponding Postman collection.

## Language
- Respond in the same language used by the user, but all names and comments added to the source code must be written in English.
- Comments in the source code must be limited to what is necessary and written in clear, concise English.
- All identifiers (classes, methods, variables, properties, fields, DTOs, etc.) must always use English naming.
- If a new user-facing text is added (UI labels, error messages, logs, notifications, etc.), it must be placed in the appropriate Resource file according to the project’s localization structure, and never hard-coded in the source code.
