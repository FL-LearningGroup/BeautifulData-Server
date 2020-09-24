## BDS Architecture
+ Diagram Mermaid Online Edit

+ Diagram View

+ Diagram Mermaid Code

```mermaid
graph TB
  bds[BDS Architecture] --> runtime[Runtime]
  bds --> framework[Framewrok]
  bds --> plugin[Plugin]
  runtime --> |Drive|framework
  framework --> |Implementation|pipline[Pipeline]
  framework --> |Use|plugin
 ```
