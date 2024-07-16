FROM cr.uni-systems.ru/univuz/univuz_3_0/node:18 as node
WORKDIR /usr/src/app

COPY package.json package-lock.json /usr/src/
RUN npm install --prefer-offline --no-audit
COPY . .
RUN npx kendo-ui-license activate
RUN npx nx run host:build:production

FROM cr.uni-systems.ru/univuz/univuz_3_0/nginx:stable
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /usr/src/app/dist/apps/host/ /usr/share/nginx/html

CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]

