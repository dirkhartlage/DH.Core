# DH Core
Core module containing common things that are required for most projects. Most of my other modules depend on this.

## Table of Contents
- [DH Core](#dh-core)
  - [Table of Contents](#table-of-contents)
  - [Compatibility](#compatibility)
  - [Dependencies](#dependencies)
  - [Features](#features)
  - [Development](#development)
  - [Planned features](#planned-features)
  - [Other packages](#other-packages)


## Compatibility
Tested with IL2CPP .Netstandard2.1 on Unity 2021.3.  
But you can always run the tests in the TestRunner yourself.

## Dependencies
Required:
- [UniTask](https://openupm.com/packages/com.cysharp.unitask/)

(Merge this into `Packages/manifest.json`)
```
    "scopedRegistries": [
        {
            "name": "package.openupm.com",
            "url": "https://package.openupm.com",
            "scopes": [
                "com.cysharp.unitask"
            ]
        }
    ],
    "dependencies": {
        "com.cysharp.unitask": "2.5.3"
    }
```
Optional:
- [Addressables](https://docs.unity3d.com/Packages/com.unity.addressables@1.21/manual/index.html)  
```
    "dependencies": {
        "com.unity.addressables": "1.21.19"
    }
```
When merging, don't forget the comma after each entry in the JSON lists.

## Features
There are several core features at the moment:  
- Dependency system
  - Allows waiting for initialization of other objects through Dependencies
  - Dependencies can be defined by implementing the Dependency class
  - There are several implementations available already, such as one for regular Tasks, for Addressables loading handles (AsyncOperationHandle) and one for IInitializable
- Advanced base classes built upon Monobehaviour
  - Singleton
    - Configurable policies: Can require being spawned from prefab
  - InitializableMonobehaviour
    - Implements IInitializable
    - Builtin dependency management
    - UniTask convenience features, such as cancellationToken management
  - InitializableSingleton
    - Singleton features combined with InitializableMonobehaviour features

There are also some more small utility things.

## Development
Core may be updated at random, whenever I need something.  
I'm open to feature request, but larger things or things that are not very universal, don't belong in here.  
These could go in additional packages however.

## Planned features
Async scene management utils.  

## Other packages
I plan to release these in the coming months, but I don't have unlimited time to work on these side projects.
- DH.MirrorUtils
  - Mirror networking related utils
- DH.Resources
  - Networked resource management (resource as in a gamedesign context)
- DH.Stats
  - An attribute system that allows status effects with timers. v1 is not networked, v2 is gonna be networked
- DH.Abilities
  - Abilities with cooldown and resource management. v2 is gonna scrap integrated resources and rely on DH.Resources.
- DH.Weapons
  - Implementation for easily setting up any projectile or hitscan weapon with custom behaviours.

Far future:  
- DH.NativeCollections
  - Extends [Collections](https://docs.unity3d.com/Packages/com.unity.collections@2.2/manual/index.html)
