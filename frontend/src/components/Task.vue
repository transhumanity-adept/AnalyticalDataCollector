<script setup lang="ts">
import router from "@/router";

export interface ITaskProps {
  id: String,
  createDate: Date,
  fromArticleId: Number,
  toArticleId: Number,
  currentArticleIdLoad: Number,
  isComplete: Boolean,
  width: String
}

defineProps<ITaskProps>()

function Start(event: any) {
  event.stopPropagation();
  console.log('Test')
}

function Stop(event: any) {
  event.stopPropagation();
  console.log('Test 2')
}

function Delete(event: any) {
  event.stopPropagation();
}
</script>

<template>
  <div
    class="task-hover pt-2 pb-2 pl-5 pr-5"
    style="transition-duration: .3s; box-shadow: 3px 3px 10px #EEEEEE; border-radius: 5px"
    :style="{ width: width }"
    @click="router.push(`task/${id}`)"
  >
    <div style="color: grey">
      {{
        createDate.toLocaleString("ru-RU",
        {
          weekday: 'long',
          year: 'numeric',
          month: 'long',
          day: 'numeric',
          hour12: false,
          hour: "numeric",
          minute: "numeric",
          second: "numeric",
          timeZoneName: "shortGeneric"
        })
      }}
    </div>
    <div class="d-flex align-center">
      <div class="d-flex justify-center align-center w-100">
        <v-progress-linear
          class="rounded-pill"
          :color="isComplete ? 'green' : 'primary'"
          :model-value="currentArticleIdLoad - fromArticleId"
          :max="toArticleId - fromArticleId"
          height="25"
          bg-color="black"
          bg-opacity="0.24"
        >
          <template v-slot:default>
          <span class="text-button text-white">
          [ {{ fromArticleId }} ... {{ currentArticleIdLoad }} ... {{ toArticleId }} ]
        </span>
          </template>
        </v-progress-linear>
      </div>
      <div class="d-flex justify-center ml-4">
        <v-btn
          flat
          color="transparent"
          @click="Start"
        >
          <template v-slot:loader>
            <v-progress-circular
              size="small"
              color="primary"
              indeterminate
              :width="3"
            />
          </template>
          <v-icon color="primary" size="26px">mdi-play</v-icon>
        </v-btn>
        <v-btn
          flat
          color="transparent"
          @click="Stop"
        >
          <template v-slot:loader>
            <v-progress-circular
              size="small"
              color="red"
              indeterminate
              :width="3"
            />
          </template>
          <v-icon color="red" size="26px">mdi-stop</v-icon>
        </v-btn>
        <v-btn
          flat
          color="transparent"
          @click="Delete"
        >
          <template v-slot:loader>
            <v-progress-circular
              size="small"
              color="red"
              indeterminate
              :width="3"
            />
          </template>
          <v-icon color="grey" size="20px">mdi-delete</v-icon>
        </v-btn>
      </div>
    </div>
  </div>
</template>

<style scoped>
.task-hover:hover {
  background: #E3F2FD;
  cursor: pointer;
}
</style>
