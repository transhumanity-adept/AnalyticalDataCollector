<script setup lang="ts">
import {ref, computed} from "vue";
import {ArticleLoadLog, IArticleLoadLog, LogType} from "@/Models/ArticleLoadLog";
import {TaskModel} from "@/Models/Task";
import TaskLoadError from "@/components/TaskLoadError.vue";
import {HollowDotsSpinner} from "epic-spinners"

export interface IProps {
  id: String
}

const props = defineProps<IProps>();

const currentDate = new Date();
const taskInfo = ref(new TaskModel(props.id, currentDate, 7, 5, 10))

const logs = ref(new Array<IArticleLoadLog>(
  new ArticleLoadLog(1, '{ "message" : "Сообщение об ошибке" }', LogType.Error),
  new ArticleLoadLog(2, '{ "articleId" : 2, "message" : "Lorem"}', LogType.Error),
));

const logOpened = ref(false);

const showedLogs = computed(() => {
  return logOpened.value ? logs.value : logs.value.slice(0, 2);
});

const address = process.env.SERVER_ADDRESS;
</script>

<template>
  <v-main class="d-flex justify-center">
    <div class="w-75 mt-6">
      <v-container>
        <v-row class="w-100 mb-5">
          <v-card class="w-100">
            <v-container>
              <div class="d-flex justify-space-between">
                <div class="d-flex text-button text-primary align-center">
                  Задача выполняется
                  <hollow-dots-spinner
                    class="ml-5"
                    :animation-duration="1500"
                    :dot-size="8"
                    :dots-num="3"
                    color="#1E88E5"
                  />
                </div>
                <div>
                  <v-btn
                    color="primary"
                    @click="Start"
                    class="mr-3"
                    flat
                  >
                    <template v-slot:loader>
                      <v-progress-circular
                        size="small"
                        color="primary"
                        indeterminate
                        :width="3"
                      />
                    </template>
                    <v-icon size="26px">mdi-play</v-icon>
                  </v-btn>
                  <v-btn
                    color="primary"
                    @click="Stop"
                    class="mr-3"
                    flat
                  >
                    <template v-slot:loader>
                      <v-progress-circular
                        size="small"
                        color="primary"
                        indeterminate
                        :width="3"
                      />
                    </template>
                    <v-icon size="26px">mdi-stop</v-icon>
                  </v-btn>
                  <v-btn
                    color="primary"
                    @click="Delete"
                    flat
                  >
                    <template v-slot:loader>
                      <v-progress-circular
                        size="small"
                        color="primary"
                        indeterminate
                        :width="3"
                      />
                    </template>
                    <v-icon size="26px">mdi-delete</v-icon>
                  </v-btn>
                </div>
              </div>
              <v-progress-linear
                color="primary"
                :max="taskInfo.toArticleId - taskInfo.fromArticleId"
                :model-value="taskInfo.currentArticleIdLoad - taskInfo.fromArticleId"
              />
              <div class="pl-2 pr-2 d-flex justify-space-between text-subtitle-2 text-grey-darken-2">
                <div>{{ taskInfo.fromArticleId }}</div>
                <div>{{ taskInfo.currentArticleIdLoad }}</div>
                <div>{{ taskInfo.toArticleId }}</div>
              </div>
            </v-container>
          </v-card>
        </v-row>
        <v-row class="w-100 mb-5">
          <v-card class="w-100 pb-5">
            <v-container class="d-flex justify-space-between align-center pb-0">
              <div class="d-flex text-button text-primary align-center">
                Сообщения о ходе выполнения задачи
              </div>
              <div class="h-100 d-flex justify-center align-center">
                <v-btn @click="logOpened = !logOpened" style="width: 200px" color="primary">
                  {{ logOpened ? 'скрыть' : 'показать все' }}
                </v-btn>
              </div>
            </v-container>
            <transition-group
              name="list"
            >
              <v-container
                v-for="(log, index) in showedLogs"
                :key="index"
                class="mb-0 pb-0"
              >
                <task-load-error
                  :message="log.errorMessage"
                  :log-type="log.logType"
                />
              </v-container>
            </transition-group>
          </v-card>
        </v-row>
        <v-row class="w-100">
          <v-card class="w-100">
            <v-container>
              <div class="d-flex justify-space-between">
                <div class="d-flex text-button text-primary align-center">
                  Собранные данные
                </div>
                <div>
                  <v-btn
                    color="primary"
                    class="mr-3"
                    flat
                  >
                    <template v-slot:loader>
                      <v-progress-circular
                        size="small"
                        color="primary"
                        indeterminate
                        :width="3"
                      />
                    </template>
                    Скачать
                  </v-btn>
                </div>
              </div>
            </v-container>
            <v-container>
            </v-container>
          </v-card>
        </v-row>
      </v-container>
    </div>
  </v-main>
</template>

<style>
.list-enter-active,
.list-leave-active {
  transition: all 0.4s ease;
}

.list-enter-from,
.list-leave-to {
  opacity: 0;
  transform: translateY(200px);
}
</style>
