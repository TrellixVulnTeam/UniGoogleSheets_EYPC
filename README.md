# UniGoogleSheets

 - 주말에 시간날때마다 작업 할 프로젝트
 
 UnityGoogleSheets의 코어 백엔드 재작업(리팩토링)
 Asset Store에 판매하기전에 완전 재작업하고 오류 0% 목표로 ..

## Goal
 - 전체 코어 리팩토링 및 성능향상
 - GUI 개선 
 - Asset Store 출시
 - Node.js 자체 서버 및 Google Scripts 동시지원 (내부 접속, 외부 접속 지원)
 - 2차원 배열 형태에대한 Row, Col에대한 입출력 인터페이스화 (엑셀, 구글시트, CSV 모두 지원)
 
 
 ## Docs
  Parser        - string value를 알맞은 타입정보에 맞게 object 형태로 파싱  
  Generator     - AST 트리 형태에서 알맞게 코드 생성  
  Service/**    - 대상 host와 통신하는 노드 서버 및 구글 스크립트 백엔드   
  IO            - 인터페이스화 된 CSV 읽기 쓰기  
  
해당 프로젝트는 이전 [UnityGoogleSheets](https://ugs.shlife.dev)와 마이그레이션(호환) 되지 않음.
