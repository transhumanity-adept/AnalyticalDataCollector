<script lang="ts" setup>
import Task from '@/components/Task.vue';
import { TaskModel, ITaskModel } from "@/Models/Task";
import { computed, ref } from "vue";

const loading = {
  uncompletedTasksLoading: false,
  completedTasksLoading: false,
  someLoading: function () {
    return this.uncompletedTasksLoading || this.completedTasksLoading
  }
}

const currentDate = new Date();

const tasks = ref([
  new TaskModel("1", currentDate,7,5,10),
  new TaskModel("2", currentDate,10,5,10),
  new TaskModel("3", currentDate,2,10,5),
  new TaskModel("4", currentDate,2,10,5)
]);

const completedTasks = computed(() => tasks.value.filter(x => x.isCompleted));
const uncompletedTasks = computed(() => tasks.value.filter(x => !x.isCompleted));

const showCompletedTasks = ref(false);

</script>

<template>
  <v-main class="d-flex justify-center">
    <div class="w-75">
      <v-container class="d-flex justify-space-between mb-0 pb-0" style="height: 80px">
        <div>
          <v-btn :disabled="loading.someLoading()"
                 :rounded="0"
                 flat
                 height="100%"
                 :color="!showCompletedTasks ? '#EEEEEE' : 'white'"
                 @click="showCompletedTasks = false"
          >
            Текущие
          </v-btn>
          <v-btn :disabled="loading.someLoading()"
                 :rounded="0"
                 flat
                 height="100%"
                 :color="showCompletedTasks ? '#EEEEEE' : 'white'"
                 @click="showCompletedTasks = true"
          >
            Выполненные
          </v-btn>
        </div>
        <div class="d-flex justify-center align-center">
          <v-btn :disabled="loading.someLoading()"
                 color="primary"
          >
            Новая задача
          </v-btn>
        </div>
      </v-container>
      <div v-if="loading.completedTasksLoading || loading.uncompletedTasksLoading">
        <v-container style="height: 600px">
          <v-row
            class="fill-height"
            align-content="center"
            justify="center"
          >
            <v-col
              class="text-button text-center"
              cols="12"
            >
              Загрузка задач
            </v-col>
            <v-col cols="8">
              <v-progress-linear
                color="primary"
                indeterminate
                rounded
                height="5"
              ></v-progress-linear>
            </v-col>
          </v-row>
        </v-container>
      </div>
      <div v-else>
        <div v-if="!showCompletedTasks">
          <v-container
            v-for="(task, index) in uncompletedTasks"
            :key="index"
          >
            <task
              :id="task.id"
              :create-date="task.createDate"
              :current-article-id-load="task.currentArticleIdLoad"
              :from-article-id="task.fromArticleId"
              :to-article-id="task.toArticleId"
              :is-complete="task.isCompleted"
              width="100%"
            />
          </v-container>
        </div>
        <div v-else>
          <v-container
            v-for="(task, index) in completedTasks"
            :key="index"
          >
            <task
              :id="task.id"
              :create-date="task.createDate"
              :current-article-id-load="task.currentArticleIdLoad"
              :from-article-id="task.fromArticleId"
              :to-article-id="task.toArticleId"
              :is-complete="task.isCompleted"
              width="100%"
            />
          </v-container>
        </div>
      </div>
    </div>
  </v-main>
</template>


<style scoped>
.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active до версии 2.1.8 */ {
  opacity: 0;
}
</style>
