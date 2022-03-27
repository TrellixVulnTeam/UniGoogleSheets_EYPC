# UniGoogleSheets

 - 주말에 시간날때마다 작업 할 프로젝트
 
 UnityGoogleSheets의 코어 백엔드 재작업(리팩토링)
 Asset Store에 판매하기전에 완전 재작업하고 오류 0% 목표로 ..
  

## Goal
 - 모든 소스코드 영문주석 
 - 코드 자체로 실행 가능한 모든 부분에 대한 오픈소스화 
 - 전체 코어 리팩토링 및 성능향상
 - GUI 개선 
 - Asset Store 출시 (GUI를 붙인 부분으로 이 경우 상업적 판매)
 - Node.js 자체 서버 및 Google Scripts 동시지원 (내부 접속, 외부 접속 지원)
 - 2차원 배열 형태에대한 Row, Col에대한 입출력 인터페이스화 (엑셀, 구글시트, CSV 모두 지원)
 
## 고민? 
 - Node.js 자체 서버 지원이 필요한 상황 (사내 보안망에서만 실행 등)
 - 기존에는 Json 형식으로 데이터를 가지고 있었는데, CSV로 변경하거나 Binary형태로 만들려고 한다. (newtonsoft json 종속성 문제로 인해서)


## Docs
 - Parser        - string value를 알맞은 타입정보에 맞게 object 형태로 파싱  
 - Generator     - AST 트리 형태에서 알맞게 코드 생성  
 - Service/**    - 대상 host와 통신하는 노드 서버 및 구글 스크립트 백엔드   
 - IO            - 인터페이스화 된 CSV 읽기 쓰기  
  
해당 프로젝트는 이전 [UnityGoogleSheets](https://ugs.shlife.dev)와 마이그레이션(호환) 되지 않음.



LICENCE

LGPL LICENCE 라이센스


별도의 추가사항

"플러그인, 라이브러리"은 본 소스코드를 사용하여 만든 dll 혹은, 소스코드의 정적,동적인 파일을 말합니다. 
"응용프로그램"은 본 소스코드에서 제공된 API를 사용하는 실행 가능한 모든 저작물입니다.

1. 본 소스코드의 수정본 혹은 추가 확장 플러그인, 라이브러리를 타인에게 소스코드/정적 동적 형태로 배포하는것을 허락하지 않습니다.   
2. 본 소스코드를 사용하 응용프로그램의 배포는 허락되며, 소스코드를 수정한경우를 포함하여 소스코드 공개 의무는 없습니다. 

