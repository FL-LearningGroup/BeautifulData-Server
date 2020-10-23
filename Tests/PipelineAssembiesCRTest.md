|TestCase|StepDescription|Result|Status|
|:---|:---|:---|:---|
|Add-one|Add the same name of assembly in the xml file|No add new pipeline|Pass|
|Add-one|Add new assembly in the xml file|Add new pipeline|Pass|
|Remove-one|Remove already exists in the pipeline|Remove pipeline|Pass|
|Remove-one|Remove don't exists in the pipeline|No remove pipeline|Pass|
|Remove-many|Remove all aleardy pipeline|Clear pipeline|Pass|
|Add-many|Add many new pipeline|Successfully Add many new pipeline|Pass|
|Clear|Clear all elements of pipeline in the xml file|No error throw,But already pipeline not was removed.|
