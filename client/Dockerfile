FROM nginx:stable-alpine as base
RUN apk add --no-cache nodejs npm
RUN npm install --global yarn
EXPOSE 80

FROM base as build
WORKDIR /build
COPY . /build/
RUN yarn install
RUN yarn run build

FROM build as release
COPY --from=build /build/dist /usr/share/nginx/html