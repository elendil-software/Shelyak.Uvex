import http from 'k6/http';
import { check,sleep } from 'k6';

export const options = {
  vus: 1,
  duration: '10m',
  minIterationDuration: '1s',
};

export default function () {
  let response = http.get('http://localhost:6562/api/v1/Spectrograph/0/focusposition');

  check(response, {
    "response code was 200": (res) => res.status === 200,
  });
  
  sleep(1);
}