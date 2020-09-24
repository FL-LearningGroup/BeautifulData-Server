## BDS Architecture
+ Diagram Mermaid Online Edit

+ Diagram View

+ Diagram Mermaid Code

```mermaid
graph TB
  bds[BDS Architecture] --> runtime[Runtime]
  bds --> framework[Framewrok]
  runtime --> |Drive|framework
  framework --> |Extension|pipline[Pipeline]
 ```
