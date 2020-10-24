# ITDDD

"도메인 주도 설계 철저 입문" 14장의 예제 코드로,
ASP.NET Core를 사용해 만들어진 웹 사이트입니다.

클린 아키텍처의 모듈 구성을 따라 구성돼 있습니다.
자세한 구성 내용은 책 본문을 참고 하십시오.


# 실행 환경

.NET Core 3.1  
Node.js 12.14.1  

# 프로젝트 실행 방법

## 실행 환경 구축

프로젝트를 실행하려면 마이크로소프트의 IDE인 Visual Studio를 사용하는 것이 가장 쉽습니다.
아래 URL에서 Visual Studio Community를 설치하십시오. 버전은 무관합니다.

https://docs.microsoft.com/ko-kr/visualstudio/install/install-visual-studio

설치를 시작하면 워크로드를 선택해야 합니다.
아래 화면과 같이 "ASP.NET 및 웹 개발"을 선택하세요.


![vsinstaller](https://github.com/flourscent/itddd/blob/images/vs_installer.png)

### Node.js 설치하기

이 프로젝트는 Node.js를 사용합니다.
아래 URL에서 인스톨러를 내려받고 실행한 다음 인스톨러의 지시에 따라 Node.js를 설치하십시오.
※버전 12.14.1에서 동작 확인 되었습니다.
https://nodejs.org/ko

## 솔루션 열기

프로젝트를 열려면 Visual Studio를 실행하고 .sln 파일을 선택해 솔루션을 엽니다.
다음 화면은 Visual Studio 2019를 실행한 시작 메뉴입니다.

![LoadingProject](https://github.com/flourscent/itddd/blob/images/load_project_1.png)

메뉴의 "프로젝트 또는 솔루션 열기"에서 .sln 파일을 선택하면 관련된 프로젝트가 IDE에 로딩됩니다.

![LoadingProject](https://github.com/flourscent/itddd/blob/images/load_project_2.png)

## 실행하기

애플리케이션을 실행할 때는 Visual Studio에서 디버그 모드로 실행합니다.
타겟을 WebApplication으로 하고 디버그 실행(F5 또는 화면에서 "IIS Express" 버튼 클릭)하면 예제 웹 애플리케이션이 실행됩니다.

![LoadingProject](https://github.com/flourscent/itddd/blob/images/load_project_3.png)

# 시스템 설정

웹 사이트에서 데이터베이스 사용 여부를 설정할 수 있는 수단입니다.

## Dependency Injection

IOC Container에 대한 인젝션 모듈은 3가지 중 하나를 선택할 수 있습니다. 

 - 인메모리 동작 모듈
 - SQL 쿼리를 사용하는 모듈
 - EntityFramework를 사용하는 모듈

사용할 모듈 설정은 appsettings.json(운영 모드 설정 파일)과 appsettings.Developmoent.json(디버그 모드 설정 파일)에서 설정할 수 있습니다.

### 인메모리 동작 모듈

appsettings.json / appsettings.Development.json 파일에서 다음과 같이 설정하십시오.
※이 설정이 기본값입니다.

```
"Dependency": {
  "setup": "InMemoryModuleDependencySetup"
}
```

### SQL 쿼리를 사용하는 모듈

appsettings.json / appsettings.Development.json 파일에서 다음과 같이 설정하십시오.
이 설정을 사용하려면 데이터베이스와 테이블을 미리 준비해야(뒤에 설명함) 합니다.

```
"Dependency": {
  "setup": "SqlConnectionDependencySetup"
}
```

### EntityFramework를 사용하는 모듈

appsettings.json / appsettings.Development.json 파일에서 다음과 같이 설정하십시오.
이 설정을 사용하려면 데이터베이스와 테이블을 미리 준비해야(뒤에 설명함) 합니다.

```
"Dependency": {
  "setup": "EFDependencySetup"
}
```

EntityFramework와 인메모리 데이터베이스를 모두 사용하는 것도 가능합니다.
다음과 같이 설정을 추가하십시오.

```
"Dependency": {
  "SetupName": "EFDependencySetup",
  "Extra": {
    "EF" : {
      "InMemory": true 
    } 
  }
}
```

## 데이터베이스

EntityFramework 및 SQL 쿼리를 사용하는 데이터베이스로 SQL Server Express를 사용합니다.
데이터베이스를 사용하기 전에 먼저 EntityFramework의 코드 퍼스트 기능을 이용해 데이터베이스를 준비합니다.

코드 퍼스트 기능을 사용해 데이터베이스를 준비하려면 appsettings.json / appsettings.Development.json 파일의 설정을 다음과 같이 수정합니다.

```
"Dependency": {
  "setup": "EFDependencySetup"
}
```

그 다음, 패키지 매니저 콘솔에서 EFInfrastructure를 대상으로 다음과 같이 마이그레이션을 생성합니다.

```
Add-Migration Initial
```

그리고 패키지 매니저 콘솔에서 데이터베이스를 업데이트합니다(Update-Database 명령 실행). 그러면 이름이 itdddContext1인 데이터베이스가 생성됩니다.

```
Update-Database
```