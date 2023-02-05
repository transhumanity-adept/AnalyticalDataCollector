<script setup lang="ts">
import {ref} from "vue";
import {LogType} from "@/Models/ArticleLoadLog";
import VueJsonPretty from "vue-json-pretty";
import "vue-json-pretty/lib/styles.css";

interface IProps {
  message: String,
  logType: LogType
}

const props = defineProps<IProps>();

function getLogType(): String {
  switch (props.logType) {
    case LogType.Error:
      return "#FFCDD2"
    case LogType.Warning:
      return "#FFF9C4"
    case LogType.Info:
      return "#BBDEFB"
  }
}

const maxTextLength: Number = 156;
const isLongText: Boolean = props.message.length >= maxTextLength;
const opened = ref(false);

</script>

<template>
  <v-card flat :color="getLogType()" class="text-grey-darken-3">
    <v-card-title class="d-flex justify-space-between">
      <div>
        <v-chip variant="tonal">
          {{ logType }}
        </v-chip>
      </div>
      <div class="align-self-end">
        <v-btn flat color="transparent" icon v-if="isLongText" @click="opened = !opened">
          <v-icon>
            {{ opened ? 'mdi-chevron-double-up' : 'mdi-chevron-double-down' }}
          </v-icon>
        </v-btn>
      </div>
    </v-card-title>
    <v-card-text>
      <vue-json-pretty :data="JSON.parse(message)" />
      <span>{{message.substring(0, maxTextLength) + " ..."}}</span>
    </v-card-text>
  </v-card>
</template>


<style>
  .v-card__loader {
    z-index: -1 !important;
  }
</style>
