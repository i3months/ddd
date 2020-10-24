# ITDDD

"도메인 주도 설계 철저 입문 : 코드와 패턴으로 밑바닥부터 이해하는 DDD"의 14장 예제 코드입니다.
1장부터 13장에 실린 예제 코드도 보실 수 있습니다.

# 각 디렉토리의 내용

## Layered

https://github.com/wikibook/ddd/tree/master/Layered

계층형 아키텍처를 준수해 구성한 코드입니다.
프레젠테이션 계층, 도메인 계층, 애플리케이션 계층, 인프라스트럭쳐 계층이 별도의 프로젝트로 구성돼 있습니다.

## Layered_UsingInternal

https://github.com/wikibook/ddd/tree/master/Layered_UsingInternal

계층형 아키텍처를 준수해 구성한 코드입니다.
internal 접근제어자를 활용해 메서드에 대한 접근을 통제하여 의도하지 않은 메서드 호출을 차단한 것이
Layerd 디렉토리의 코드와 차이점입니다.

## CleanLike

https://github.com/wikibook/ddd/tree/master/CleanLike

클린 아키텍처를 준수해 구성한 코드입니다.
모든 유스케이스가 별도의 클래스로 구현되었으므로 코드의 결합이 느슨하게 구현돼 있습니다.

## SampleCodes

14장을 제외한 나머지 장에 실린 예제 코드입니다.
